using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDrivableCar : MonoBehaviour
{
    public GameObject vehicleParentObject;
    public CarController2 carController;

    [SerializeField] ObjectRotator objectRotator;

    public void CreateVehicle()
    {
        objectRotator.enabled = false;
        carController.enabled = true;
        carController.playerRigidBody.isKinematic = false;
    }
}
