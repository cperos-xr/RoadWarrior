using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreatedCarManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Weapon weapon;
    public Vehicle vehicle;
    public Wheel wheel;

    public Transform vehicleSpawn;
    public GameObject currentVehicle;
    public Transform wheelSpawn;
    public GameObject currentWheels;

    public Transform weaponSpawn;
    public GameObject currentWeapon;

    [SerializeField] CarController2 carController;

    [SerializeField] private WheelTransforms currentWheelTransforms;

    public delegate void WheelsSelected(WheelSet wheelSet);
    public static event WheelsSelected OnWheelsSelected;

    public delegate void VehicleBodySelected(Vehicle vehicleBody);
    public static event VehicleBodySelected OnVehicleBodySelected;

    public delegate void PrimaryWeaponSelected(Weapon primaryWeapon);
    public static event PrimaryWeaponSelected OnPrimaryWeaponSelected;

    public bool isXR;

    private void OnEnable()
    {
        ScrollViewController.OnSelectedVehiclePart += OnSelectedPart;
        if (isXR)
        {
            ScrollImageXR.OnSelectedVehiclePart += OnSelectedPart;
        }
    }

    private void OnDisable()
    {
        ScrollViewController.OnSelectedVehiclePart -= OnSelectedPart;
        if (isXR)
        {
            ScrollImageXR.OnSelectedVehiclePart -= OnSelectedPart;
        }
    }

    private void OnSelectedPart(VehiclePart selectedPart)
    {
        if (selectedPart is Weapon selectedWeapon)
        {
            weapon = selectedWeapon;
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }
            currentWeapon = Instantiate(weapon.model, weaponSpawn);
            FireWeapon fireWeapon = currentWeapon.GetComponent<FireWeapon>();
            OnPrimaryWeaponSelected?.Invoke(weapon);
        }
        else if (selectedPart is Vehicle selectedVehicle)
        {
            vehicle = selectedVehicle;
            if (currentVehicle != null)
            {
                Destroy(currentVehicle);
            }
            currentVehicle = Instantiate(vehicle.model, vehicleSpawn);
            OnVehicleBodySelected?.Invoke(vehicle);
        }
        else if (selectedPart is Wheel selectedWheel)
        {
            wheel = selectedWheel;
            if (currentWheels != null)
            {
                Destroy(currentWheels);
            }
            currentWheels = Instantiate(wheel.model, wheelSpawn);
            WheelSet wheelSet = currentWheels.GetComponent<WheelSet>();
            currentWheelTransforms = wheelSet.wheelTransforms;
            OnWheelsSelected?.Invoke(wheelSet);
        }

    }
}
