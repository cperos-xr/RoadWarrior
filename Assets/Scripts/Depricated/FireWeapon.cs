using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectile;
    public GameObject muzzleFlash;

    public void Fire()
    {
        Instantiate(projectile, firePoint.position, firePoint.rotation);

        GameObject flashInstance = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
        Destroy(flashInstance, 0.1f);

    }

}
