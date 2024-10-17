using UnityEngine;

[CreateAssetMenu(fileName = "NewVehicleBody", menuName = "Vehicle Parts/Vehicle Body")]
public class Vehicle : VehiclePart
{
    [Header("Vehicle Stats")]
    public float topSpeed;
    public float acceleration;

    public float motorForce;
    public float brakeForce;
    public float maxSteerAngle;

    public float mass;
    public float drag;
    public float angularDrag;

    [Header("Wheel Scale")]
    public Vector3 frontWheelScale = Vector3.one;
    public Vector3 rearWheelScale = Vector3.one;

    [Header("Collider")]
    public Vector3 center = Vector3.one;
    public Vector3 size = Vector3.one;

    [Header("Vehicle Type")]
    public VehicleType vehicleType;
}
