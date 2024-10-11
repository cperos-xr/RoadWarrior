using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDrivableCar : MonoBehaviour
{
    public GameObject vehicleParentObject;

    [SerializeField] ObjectRotator objectRotator;
    private void OnEnable()
    {
        CreatedCarManager.OnVehicleCreated += OnVehicleCreated;
    }

    private void OnDisable()
    {
        CreatedCarManager.OnVehicleCreated -= OnVehicleCreated;
    }

    private void OnVehicleCreated(VehicleComponents vehicleComponents, Rigidbody rigidbody)
    {
        objectRotator.enabled = false;
        VehicleController vehicleController = vehicleParentObject.AddComponent<VehicleController>();
        vehicleController.vehicleComponents = vehicleComponents;
        vehicleController.rigidBody = rigidbody;
    }
}
