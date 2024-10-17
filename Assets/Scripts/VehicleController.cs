using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public Rigidbody rigidBody;
    private float horizontalInput;
    private float verticalInput;

    [SerializeField] private float motorForce = 1500f;
    [SerializeField] private float wheelSpinSpeed = -5000f;  // Speed to spin the wheels visually
    [SerializeField] private float maxSteerAngle = 30f;    // Max steering angle

    // Use the new VehicleComponents class
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
        UpdateWheelVisuals();  // Make sure wheels spin visually
    }

    private void HandleMotor()
    {
        // Apply motor torque to the rear wheels based on input (purely for physics if needed)
        vehicleComponents.rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        vehicleComponents.rearRightWheelCollider.motorTorque = verticalInput * motorForce;
    }

    private void UpdateWheelVisuals()
    {
        // Rotate the wheels based on input without worrying about WheelCollider position
        RotateWheel(vehicleComponents.frontLeftWheelTransform);
        RotateWheel(vehicleComponents.frontRightWheelTransform);
        RotateWheel(vehicleComponents.rearLeftWheelTransform);
        RotateWheel(vehicleComponents.rearRightWheelTransform);

        // Steer the front wheels based on input
        SteerWheel(vehicleComponents.frontLeftWheelTransform);
        SteerWheel(vehicleComponents.frontRightWheelTransform);
    }

    private void SteerWheel(Transform wheelTransform)
    {
        // Steer the wheel by rotating it around its local Y-axis based on horizontalInput
        float steerAngle = maxSteerAngle * horizontalInput;  // Calculate the desired steer angle
        Vector3 newRotation = wheelTransform.localEulerAngles;
        newRotation.y = steerAngle;  // Update the Y rotation to visually steer the wheel
        newRotation.z = 0;  // Keep the Z rotation at 0
        wheelTransform.localEulerAngles = newRotation;
    }

    private void RotateWheel(Transform wheelTransform)
    {
        wheelTransform.localRotation = wheelTransform.localRotation * Quaternion.Euler(verticalInput * wheelSpinSpeed * Time.deltaTime, 0, 0);

    }

    private void HandleSteeringInput(float steerInput)
    {
        horizontalInput = steerInput;
    }

    private void HandleMovementInput(float moveInput)
    {
        verticalInput = moveInput;
    }

    private void HandleBrakingInput(float brakeInput)
    {
        // In this simplified version, we'll ignore braking for now
    }
}
