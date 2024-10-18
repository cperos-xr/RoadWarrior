using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Weapon primaryWeapon;
    [SerializeField] private FireWeapon fireWeapon;


    private Coroutine fireCoroutine;  // To track the firing coroutine
    private bool isFiringActive = false;  // To track if firing is active

    private void OnEnable()
    {
        CreatedCarManager.OnPrimaryWeaponSelected += OnPrimaryWeaponSelected;
        InputManager.OnFire += HandleFireInput;
    }

    private void OnDisable()
    {
        CreatedCarManager.OnPrimaryWeaponSelected -= OnPrimaryWeaponSelected;
        InputManager.OnFire -= HandleFireInput;
    }


    private void HandleFireInput(float isFiring)
    {
        if (isFiring > 0 && !isFiringActive)
        {
            // Start firing if not already firing
            isFiringActive = true;
            fireCoroutine = StartCoroutine(FireCoroutine());
        }
        else if (isFiring <= 0 && isFiringActive)
        {
            // Stop firing when input is released
            isFiringActive = false;
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
            }
        }
    }

    private IEnumerator FireCoroutine()
    {
        while (isFiringActive)
        {
            fireWeapon.Fire();
            yield return new WaitForSeconds(primaryWeapon.fireRate);  // Fire based on weapon's fire rate
        }
    }

    private void OnPrimaryWeaponSelected(Weapon selectedPrimaryWeapon, FireWeapon fireSelectedWeapon)
    {
        primaryWeapon = selectedPrimaryWeapon;
        fireWeapon = fireSelectedWeapon;
    }
}
