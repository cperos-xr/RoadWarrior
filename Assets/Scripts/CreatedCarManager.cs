using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedCarManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Weapon weapon;
    public Vehicle vehicle;
    public Wheel wheel;

    private void OnEnable()
    {
        ScrollViewController.OnSelectedVehiclePart += OnSelectedWeapon;
        
    }

    private void OnDisable()
    {
        ScrollViewController.OnSelectedVehiclePart -= OnSelectedWeapon;

    }

    private void OnSelectedWeapon(VehiclePart selectedPart)
    {
        if (selectedPart is Weapon selectedWeapon)
        {
            weapon = selectedWeapon;
        }
        if (selectedPart is Vehicle selectedVehicle)
        {
            vehicle = selectedVehicle;
        }
        if (selectedPart is Wheel selectedWheel)
        {
            wheel = selectedWheel;
        }
        else
        {
            Debug.Log("Selected part was not of known type");
        }
    }
}
