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

    public int numberOfWheels;
    public VehicleType vehicleType;
}
