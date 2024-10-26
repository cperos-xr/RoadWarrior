using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController2))]
public class AiCarController : MonoBehaviour
{
    public WayPointContainer waypointContainer;
    public List<Transform> waypoints;
    public int currentWaypoint;
    [SerializeField] private CarController2 carController;
    public float waypointRange;
    private float steeringInput;
    private float gasInput;
    private float brakeInput;
    public float maximumSpeed = 120f;
    public float recommendedMaxSpeed;
    public float maxBrakeInput = 0.5f;
    public float minGasInput = 0.2f;
    public float Kp = 0.01f;

    public bool isInsideBraking;

    // Recovery variables
    private float stuckSpeedThreshold = 0.1f;
    private float stuckTimeThreshold = 2f;
    private float stuckTimer = 0f;
    private bool isRecovering = false;
    private float recoveryDuration = 5f;
    private float recoveryTimer = 0f;
    private float minRecoveryTime = 2f;

    public float maxSteeringAngle = 30f; // Maximum steering angle in degrees

    void Start()
    {
        recommendedMaxSpeed = maximumSpeed;
        waypoints = waypointContainer.wayPoints;
        currentWaypoint = 0;
    }

    void Update()
    {
        // Waypoint Navigation Logic
        if (Vector3.Distance(waypoints[currentWaypoint].position, transform.position) < waypointRange)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
        }

        // Update stuck timer
        if (carController.speed < stuckSpeedThreshold)
        {
            stuckTimer += Time.deltaTime;
        }
        else
        {
            stuckTimer = 0f;
        }

        // Check if the car is stuck
        if (stuckTimer >= stuckTimeThreshold && !isRecovering)
        {
            isRecovering = true;
            recoveryTimer = 0f;
        }

        if (isRecovering)
        {
            HandleRecovery();
        }
        else
        {
            // Normal driving behavior
            CalculateInputs();
            carController.SetInput(gasInput, steeringInput, brakeInput);
        }

        // Debugging
        Debug.DrawRay(transform.position, waypoints[currentWaypoint].position - transform.position, Color.yellow);
    }

    void CalculateInputs()
    {
        Vector3 directionToWaypoint = waypoints[currentWaypoint].position - transform.position;
        Vector3 fwd = transform.forward;
        steeringInput = Vector3.SignedAngle(fwd, directionToWaypoint, Vector3.up) / 180f;

        // Determine Desired Speed
        float desiredSpeed = isInsideBraking ? recommendedMaxSpeed : maximumSpeed;

        // Calculate Speed Difference
        float speedDifference = desiredSpeed - carController.speed;

        // Proportional Control
        float throttleInput = minGasInput + speedDifference * Kp;

        // Initialize gasInput and brakeInput
        gasInput = 0f;
        brakeInput = 0f;

        if (speedDifference >= 0f)
        {
            // Need to accelerate or maintain speed
            gasInput = Mathf.Clamp(throttleInput, minGasInput, 1f);
            brakeInput = 0f;
        }
        else
        {
            // Need to decelerate
            gasInput = minGasInput; // Maintain minimum throttle to counteract friction
            brakeInput = Mathf.Clamp(-throttleInput, 0f, maxBrakeInput);
        }

        // Smooth Gas Input
        gasInput = Mathf.Lerp(carController.gasInput, gasInput, Time.deltaTime * 3f);
    }

    void HandleRecovery()
    {
        recoveryTimer += Time.deltaTime;

        // Apply reverse throttle
        gasInput = -0.5f;
        brakeInput = 0f;

        // Calculate the direction to the waypoint
        Vector3 directionToWaypoint = waypoints[currentWaypoint].position - transform.position;

        // Calculate the angle between the car's forward direction and the direction to the waypoint
        float angleToWaypoint = Vector3.SignedAngle(transform.forward, directionToWaypoint, Vector3.up);

        // Since we are reversing, invert the steering input
        steeringInput = -Mathf.Clamp(angleToWaypoint / maxSteeringAngle, -1f, 1f);

        carController.SetInput(gasInput, steeringInput, brakeInput);

        // Continue recovery until minimum time has elapsed
        if (recoveryTimer >= minRecoveryTime)
        {
            bool canExitRecovery = (CanSeeNextWaypoint() && IsPathClear()) || recoveryTimer >= recoveryDuration;

            if (canExitRecovery)
            {
                isRecovering = false;
                stuckTimer = 0f;
                recoveryTimer = 0f;
            }
        }
    }

    bool CanSeeNextWaypoint()
    {
        Vector3 directionToWaypoint = waypoints[currentWaypoint].position - transform.position;
        float angleToWaypoint = Vector3.Angle(transform.forward, directionToWaypoint);

        // Check if the car is approximately facing the waypoint (within 30 degrees)
        if (angleToWaypoint < 30f)
        {
            float distanceToWaypoint = directionToWaypoint.magnitude;
            directionToWaypoint.Normalize();

            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToWaypoint, out hit, distanceToWaypoint))
            {
                if (hit.collider.CompareTag("Waypoint"))
                {
                    return true;
                }
            }
            else
            {
                // No obstacle blocking the view to the waypoint
                return true;
            }
        }

        // Car is not facing the waypoint or obstacle detected
        return false;
    }

    bool IsPathClear()
    {
        float checkDistance = 5f; // Distance to check for obstacles
        Vector3 fwd = transform.forward;

        if (!Physics.Raycast(transform.position, fwd, checkDistance))
        {
            // Path is clear
            return true;
        }
        return false;
    }
}