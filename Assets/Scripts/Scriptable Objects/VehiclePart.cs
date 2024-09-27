using UnityEngine;

public abstract class VehiclePart : ScriptableObject
{
    [Header("General Info")]
    public string partName;
    public Texture2D icon;
    public GameObject model;
    public float weight;
    public float cost;
}
