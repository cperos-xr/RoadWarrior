using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Vehicle Parts/Weapon")]
public class Weapon : VehiclePart
{
    [Header("Weapon Stats")]
    public float damage;
    public float range;
    public float fireRate;
    public AmmoType ammoType;

    [Header("Mounting")]
    public MountPointType mountPoint;
}
