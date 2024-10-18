using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Vehicle Parts/Weapon")]
public class Weapon : VehiclePart
{
    [Header("Weapon Stats")]
    public float damage;
    public float range;
    public float fireRate;
    public bool isAutomatic;
    public int magazineSize;
    public float reloadTime;

    public AmmoType ammoType;

    [Header("Burst Settings")]
    public float horizontalSpread;
    public float verticalSpread;
    public float burstDelay;
    public int bulletsPerShot = 1;

    [Header ("Bullet Holes")]
    public GameObject bulletHolePrefab;
    public float bulletHoleLifespan;

    [Header("Mounting")]
    public MountPointType mountPoint;



}
