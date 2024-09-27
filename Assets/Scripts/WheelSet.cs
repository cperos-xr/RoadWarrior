using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSet : MonoBehaviour
{
    public Wheel wheels;

    public WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider, rearRightWheelCollider;
    public Transform frontLeftWheelTransform, frontRightWheelTransform;
    public Transform rearLeftWheelTransform, rearRightWheelTransform;
}
