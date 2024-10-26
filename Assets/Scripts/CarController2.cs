using UnityEngine;

public class CarController2 : MonoBehaviour
{
    public Rigidbody playerRigidBody;

    public WheelColliders wheelColliders;
    public WheelTransforms wheelTransforms;

    public float gasInput;
    public float steerInput;
    public float brakeInput;

    public float motorForce;
    public float brakeForce;
    public float speed;

    public float maxSteering = 70f;

    public AnimationCurve steeringCurve;

    [SerializeField] private PlayerCarInput input;

    // Flag to indicate whether this is an AI-controlled car
    private bool isAIControlled;

    private void OnEnable()
    {
        isAIControlled = input == null;

        if (!isAIControlled)
        {
            InputManager.OnSteer += HandleSteeringInput;
            InputManager.OnMove += HandleMovementInput;
            InputManager.OnBrake += HandleBrakingInput;
        }
    }

    private void OnDisable()
    {
        if (!isAIControlled)
        {
            InputManager.OnSteer -= HandleSteeringInput;
            InputManager.OnMove -= HandleMovementInput;
            InputManager.OnBrake -= HandleBrakingInput;
        }
    }

    void Update()
    {
        speed = playerRigidBody.velocity.magnitude;

        if (isAIControlled)
        {
            // AI-controlled car: Inputs are set via SetInput()
            // brakeInput is set directly by the AI
        }
        else
        {
            // Player-controlled car: Get inputs from PlayerCarInput
            gasInput = input.throttleDampened;
            steerInput = input.steeringDampened;
            brakeInput = input.brakeInput;

            // Apply internal brake logic for the player
            CheckInput();
        }

        ApplyMotorForce();
        ApplySteering();
        ApplyBrakeForce();
        ApplyWheelPositions();
    }

    public void SetInput(float throttleIn, float steeringIn, float brakeIn)
    {
        gasInput = throttleIn;
        steerInput = steeringIn;
        brakeInput = brakeIn;
    }

    void ApplySteering()
    {
        float steerAngle = steerInput * steeringCurve.Evaluate(speed);
        wheelColliders.frontLeftWheelCollider.steerAngle = steerAngle;
        wheelColliders.frontRightWheelCollider.steerAngle = steerAngle;
    }

    void ApplyMotorForce()
    {
        wheelColliders.rearLeftWheelCollider.motorTorque = gasInput * motorForce;
        wheelColliders.rearRightWheelCollider.motorTorque = gasInput * motorForce;
    }

    void ApplyWheelPositions()
    {
        UpdateWheel(wheelColliders.frontLeftWheelCollider, wheelTransforms.frontLeftWheelTransform);
        UpdateWheel(wheelColliders.frontRightWheelCollider, wheelTransforms.frontRightWheelTransform);
        UpdateWheel(wheelColliders.rearLeftWheelCollider, wheelTransforms.rearLeftWheelTransform);
        UpdateWheel(wheelColliders.rearRightWheelCollider, wheelTransforms.rearRightWheelTransform);
    }

    void ApplyBrakeForce()
    {
        wheelColliders.frontLeftWheelCollider.brakeTorque = brakeInput * brakeForce * 0.7f;
        wheelColliders.frontRightWheelCollider.brakeTorque = brakeInput * brakeForce * 0.7f;

        wheelColliders.rearLeftWheelCollider.brakeTorque = brakeInput * brakeForce * 0.3f;
        wheelColliders.rearRightWheelCollider.brakeTorque = brakeInput * brakeForce * 0.3f;
    }

    void UpdateWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Quaternion quaternion;
        Vector3 position;
        wheelCollider.GetWorldPose(out position, out quaternion);
        wheelTransform.position = position;
        wheelTransform.rotation = quaternion;
    }

    private void HandleMovementInput(float value)
    {
        gasInput = value;
    }

    private void HandleSteeringInput(float value)
    {
        steerInput = value;
    }

    private void HandleBrakingInput(float value)
    {
        brakeInput = Mathf.Abs(value);
    }

    private void CheckInput()
    {
        // Internal brake logic for player control
        float movingDirection = Vector3.Dot(transform.forward, playerRigidBody.velocity);
        if (movingDirection < -0.5f && gasInput > 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else if (movingDirection > 0.5f && gasInput < 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else
        {
            // Use player's brake input
            brakeInput = input.brakeInput;
        }
    }
}
