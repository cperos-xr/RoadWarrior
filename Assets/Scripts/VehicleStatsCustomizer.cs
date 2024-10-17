using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreatedCarManager;

public class VehicleStatsCustomizer : MonoBehaviour
{
    [SerializeField] private CarController2 carController;
    public BoxCollider carBoxCollider;

    private void OnEnable()
    {
        CreatedCarManager.OnVehicleBodySelected += OnVehicleBodySelected;
    }
    private void OnDisable()
    {
        CreatedCarManager.OnVehicleBodySelected -= OnVehicleBodySelected;
    }

    private void OnVehicleBodySelected(Vehicle selectedVehicleBody)
    {
        carController.playerRigidBody.mass = selectedVehicleBody.mass;
        carController.playerRigidBody.drag = selectedVehicleBody.drag;
        carController.playerRigidBody.angularDrag = selectedVehicleBody.angularDrag;
        carController.motorForce = selectedVehicleBody.motorForce;
        carController.brakeForce = selectedVehicleBody.brakeForce;

        SetBoxCollider(selectedVehicleBody.center, selectedVehicleBody.size);
    }

    private void SetBoxCollider(Vector3 center, Vector3 size)
    {
        carBoxCollider.center = center;
        carBoxCollider.size = size;
    }
}
