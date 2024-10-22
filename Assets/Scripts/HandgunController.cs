using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HandgunController : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Transform originalParent;
    private Rigidbody rb;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        originalParent = transform.parent;
        rb = GetComponent<Rigidbody>();

        // Subscribe to events with the correct argument types
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Unparent the gun when picked up
        transform.SetParent(null);
        rb.isKinematic = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Reparent the gun to the holder when released
        transform.SetParent(originalParent);
        rb.isKinematic = true; // Make it follow the car smoothly
    }

    void OnDestroy()
    {
        // Unsubscribe from events to avoid memory leaks
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}
