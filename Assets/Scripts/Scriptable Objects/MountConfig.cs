using UnityEngine;

[CreateAssetMenu(fileName = "MountConfig", menuName = "Mount/MountConfig", order = 1)]
public class MountConfig : ScriptableObject
{
    public string mountType; // Optional: purely for identifying the config in the editor
    public Vector3 positionOffset;
    public Quaternion rotationOffset;
}
