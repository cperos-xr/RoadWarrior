using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBrakeForce;
    private bool isBraking;

    [SerializeField] private float motorForce = 1500f;
    [SerializeField] private float brakeForce = 3000f;
    [SerializeField] private float maxSteerAngle = 30f;

    public VehicleComponents vehicleComponents;

    private void OnEnable()
    {
        InputManager.OnSteer += HandleSteeringInput;
        InputManager.OnMove += HandleMovementInput;
        InputManager.OnBrake += HandleBrakingInput;
    }

    private void OnDisable()
    {
        InputManager.OnSteer -= HandleSteeringInput;
        InputManager.OnMove -= HandleMovementInput;
        InputManager.OnBrake -= HandleBrakingInput;
    }

    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void HandleSteeringInput(float value)
    {
        horizontalInput = value;
    }

    private void HandleMovementInput(float value)
    {
        verticalInput = value;
    }

    private void HandleBrakingInput(bool isBrakingInput)
    {
        isBraking = isBrakingInput;
    }

    private void HandleMotor()
    {
        vehicleComponents.rearLeftWheelCollider.motorTorque = verticalInput * motorForce * -1;
        vehicleComponents.rearRightWheelCollider.motorTorque = verticalInput * motorForce * -1;

        currentBrakeForce = isBraking ? brakeForce : 0f;
        ApplyBraking();
    }

    private void ApplyBraking()
    {
        vehicleComponents.frontRightWheelCollider.brakeTorque = currentBrakeForce;
        vehicleComponents.frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        vehicleComponents.rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        vehicleComponents.rearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        vehicleComponents.frontLeftWheelCollider.steerAngle = currentSteerAngle;
        vehicleComponents.frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(vehicleComponents.frontLeftWheelCollider, vehicleComponents.frontLeftWheelTransform);
        UpdateSingleWheel(vehicleComponents.frontRightWheelCollider, vehicleComponents.frontRightWheelTransform);
        UpdateSingleWheel(vehicleComponents.rearRightWheelCollider, vehicleComponents.rearRightWheelTransform);
        UpdateSingleWheel(vehicleComponents.rearLeftWheelCollider, vehicleComponents.rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
