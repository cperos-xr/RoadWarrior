using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDrivableCar : MonoBehaviour
{
    public GameObject vehicleParentObject;
    public CarController2 carController;

    [SerializeField] private ObjectRotator objectRotator;

    [SerializeField] private Transform PlayerLocation;
    [SerializeField] private GameObject XROrigin;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private CameraController cameraController;
    public List <GameObject> deactivateOnCreate;
    public List<GameObject> activateOnCreate;


    public bool isXR = false;

    public delegate void DrivableCarCreated();
    public static event DrivableCarCreated OnDrivableCarCreated;

    public void CreateVehicle()
    {
        objectRotator.enabled = false;
        carController.enabled = true;
        carController.playerRigidBody.isKinematic = false;

        if (isXR)
        {
            PlacePlayerInVehicle();
        }

    }

    // Place the player in the vehicle
    public void PlacePlayerInVehicle()
    {
        characterController.enabled = false;
        //cameraController.enabled = true;

        foreach (GameObject obj in deactivateOnCreate)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in activateOnCreate)
        {
            obj.SetActive(true);
        }

        OnDrivableCarCreated?.Invoke();

        //// Set the XR Origin to the player spawn locaction and make it a child of the player spawn location
        //XROrigin.transform.SetParent(PlayerLocation);
        //XROrigin.transform.position = PlayerLocation.position;
        //XROrigin.transform.rotation = PlayerLocation.rotation;
    }
}
