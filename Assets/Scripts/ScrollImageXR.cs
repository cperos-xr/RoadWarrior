using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollImageXR : ScrollImage
{
    public delegate void SelectedObject(VehiclePart selectedPart);
    public static event SelectedObject OnSelectedVehiclePart;
    private void OnEnable()
    {
        OnSelectedVehiclePart.Invoke(part);
    }

    private void Start()
    {
        OnSelectedVehiclePart.Invoke(part);
    }
}
