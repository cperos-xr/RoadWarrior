using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class XROriginPlacer : MonoBehaviour
{
    public XROrigin xrOrigin;
    public Transform targetTransform;

    void Start()
    {
        if (xrOrigin == null || targetTransform == null)
        {
            Debug.LogError("XR Origin or Target Transform not assigned.");
            return;
        }

        PlaceXROrigin();
    }

    [ContextMenu("Place XR Origin")] // Optional: Adds a right-click option in the inspector
    public void PlaceXROrigin()
    {
        InputTracking.disablePositionalTracking = true;
        xrOrigin.transform.position = targetTransform.position;
        xrOrigin.transform.rotation = targetTransform.rotation;
    }
}
