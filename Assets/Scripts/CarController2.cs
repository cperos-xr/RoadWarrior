using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController2 : MonoBehaviour
{
    public Rigidbody playerRigidBody;
    public VehicleComponents wheelComponents;
    public float gasInput;
    public float steerInput;
    public float brakeInput;

    public float motorForce;
    public float brakeForce;
    public float slipAngle;
    private float speed;

    public AnimationCurve steeringCurve;


    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
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
        wheelComponents.frontLeftWheelCollider.steerAngle = steerAngle;
        wheelComponents.frontRightWheelCollider.steerAngle = steerAngle;

    }

    void ApplyMotorForce()
    {
        wheelComponents.rearLeftWheelCollider.motorTorque = gasInput * motorForce;
        wheelComponents.rearRightWheelCollider.motorTorque = gasInput * motorForce;
    }

    void ApplyWheelPositions()
    {
        UpdateWheel(wheelComponents.frontLeftWheelCollider, wheelComponents.frontLeftWheelTransform);
        UpdateWheel(wheelComponents.frontRightWheelCollider, wheelComponents.frontRightWheelTransform);
        UpdateWheel(wheelComponents.rearLeftWheelCollider, wheelComponents.rearLeftWheelTransform);
        UpdateWheel(wheelComponents.rearRightWheelCollider, wheelComponents.rearRightWheelTransform);
    }

    void ApplyBrakeForce()
    {
        wheelComponents.frontLeftWheelCollider.brakeTorque = brakeInput * brakeForce * 0.7f;
        wheelComponents.frontRightWheelCollider.brakeTorque = brakeInput * brakeForce * 0.7f;

        wheelComponents.rearLeftWheelCollider.brakeTorque = brakeInput * brakeForce * 0.3f;
        wheelComponents.rearRightWheelCollider.brakeTorque = brakeInput * brakeForce * 0.3f;
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

    private void HandleBrakingInput(double isBraking)
    {
        brakeInput = Mathf.Abs((float)isBraking);
    }

    private void CheckInput()
    {
        slipAngle = Vector3.Angle(transform.forward, playerRigidBody.velocity - transform.forward);
        if (slipAngle < 120f)
        {
            if (gasInput > 0)
            {
                brakeInput = 0;
            }
        }
    }
}
