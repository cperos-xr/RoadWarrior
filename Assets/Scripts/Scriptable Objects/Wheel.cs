using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWheel", menuName = "Vehicle Parts/Wheel")]
public class Wheel : VehiclePart
{
    [Header("Wheel Scale")]
    Vector3 frontWheelScale;
    Vector3 rearWheelScale;

    [Header("Wheel Stats")]
    public float hitPoints;
    public float radius;
    public float topSpeedModifier;
    public float accelerationModifier;

    public float wheelDampingRate;
    public float suspensionDistance;

    [Header("Suspension")]
    public SuspensionSpring suspensionSpring;


    [Header("Friction")]
    public Friction forwardFriction;
    public Friction sidewaysFriction;

    [Header("Compatibility")]
    public List<VehicleType> compatibleVehicleTypes;

}

[Serializable]
public class Friction
{
    public float extremumSlip;
    public float extremumValue;

    public float asymptoteSlip;
    public float asymptoteValue;
    public float stiffness;

}

[Serializable]
public class SuspensionSpring
{
    public float spring;
    public float damper;
}

