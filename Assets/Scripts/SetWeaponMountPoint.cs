using UnityEngine;

public class SetMountPoint : MonoBehaviour
{
    public MountConfig mountConfig; // This should match the mountConfig of the correct mount point


    private void OnEnable()
    {
        WeaponMountPoint.OnChangeMountPoint += SetMyPosition;
    }

    private void OnDisable()
    {
        WeaponMountPoint.OnChangeMountPoint -= SetMyPosition;
    }

    private void SetMyPosition(GameObject mountPoint, MountConfig mountPointConfig)
    {
        // Check if the mount point's config matches the object's config
        if (mountConfig == mountPointConfig)
        {
            Debug.Log("Mount point matched: " + mountPoint.name + " for " + gameObject.name, mountPoint);

            Transform originalParent = transform.parent;

            // Set as a child of the mount point and adjust position and rotation
            transform.SetParent(mountPoint.transform);
            transform.localPosition = mountConfig.positionOffset;
            transform.localRotation = mountConfig.rotationOffset;

            // Return to the original parent
            transform.SetParent(originalParent);

        }
        else
        {
            Debug.Log("Mount point config does not match the object's config. Skipping.", mountPoint);
        }
    }
}
