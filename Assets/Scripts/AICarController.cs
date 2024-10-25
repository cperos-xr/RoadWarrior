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
    public bool isInsideBraking;
    public float maximumAngle = 45f;
    public float maximumSpeed = 120f;
    public float recommendedMaxSpeed;
    public float scalingFactor = 10f; // Adjust this value based on your game's dynamics

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

        Vector3 directionToWaypoint = waypoints[currentWaypoint].position - transform.position;
        Vector3 fwd = transform.forward;
        steeringInput = Vector3.SignedAngle(fwd, directionToWaypoint, Vector3.up) / 180f;

        // Determine Desired Speed
        float desiredSpeed = isInsideBraking ? recommendedMaxSpeed : maximumSpeed;

        // Calculate Speed Difference
        float speedDifference = carController.speed - desiredSpeed;

        // Adjust Brake Input Proportionally
        if (speedDifference > 0)
        {
            brakeInput = Mathf.Clamp(speedDifference / scalingFactor, 0f, 1f);
            gasInput = 0f; // Reduce throttle when braking
        }
        else
        {
            brakeInput = 0f;
            // Adjust gasInput based on how close you are to desired speed
            gasInput = Mathf.Clamp01(1f - (carController.speed / desiredSpeed));
        }

        // Smooth Gas Input
        gasInput = Mathf.Lerp(carController.gasInput, gasInput, Time.deltaTime * 3f);

        // Pass Inputs to Car Controller
        carController.SetInput(gasInput, steeringInput, recommendedMaxSpeed, isInsideBraking);

        // Debugging
        Debug.DrawRay(transform.position, directionToWaypoint, Color.yellow);
    }
}
