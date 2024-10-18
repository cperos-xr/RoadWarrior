using System;
using System.Collections;
using System.Collections.Generic;
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
    public float slipAngle;
    private float speed;

    public AnimationCurve steeringCurve;

    [SerializeField] private PlayerCarInput input;

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (input == null)
        {
            InputManager.OnSteer += HandleSteeringInput;
            InputManager.OnMove += HandleMovementInput;
            InputManager.OnBrake += HandleBrakingInput;
        }

    }

    private void OnDisable()
    {
        if (input == null)
        {
            InputManager.OnSteer -= HandleSteeringInput;
            InputManager.OnMove -= HandleMovementInput;
            InputManager.OnBrake -= HandleBrakingInput;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (input != null)
        {
            gasInput = input.throttleDampened;
            steerInput = input.steeringDampened;
            brakeInput = input.brakeInput;
        }
        speed = playerRigidBody.velocity.magnitude;
        CheckInput();
        ApplyMotorForce();
        ApplySteering();
        ApplyBrakeForce();
        ApplyWheelPositions();
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

    private void HandleBrakingInput(float isBraking)
    {
        brakeInput = Mathf.Abs(isBraking);
    }

    private void CheckInput()
    {
        slipAngle = Vector3.Angle(transform.forward, playerRigidBody.velocity - transform.forward);
        //fixed code to brake even after going on reverse by Andrew Alex 
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
            brakeInput = 0;
        }
    }
}
