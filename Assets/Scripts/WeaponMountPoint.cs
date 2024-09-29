using UnityEngine;

public class WeaponMountPoint : MonoBehaviour
{
    public MountConfig mountConfig; // Reference to a specific mount configuration

    public delegate void ChangeMountPoint(GameObject myMountPoint, MountConfig mountConfig);
    public static event ChangeMountPoint OnChangeMountPoint;

    private void OnEnable()
    {
        OnChangeMountPoint?.Invoke(gameObject, mountConfig);
    }
}
