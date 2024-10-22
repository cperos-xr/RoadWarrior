using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MatchParentTransform : MonoBehaviour
{
    public bool matchPosition = true;
    private XRGrabInteractable grabInteractable;
    private Transform originalParent;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        originalParent = transform.parent;

        // Subscribe to events with the correct argument types
        grabInteractable.selectEntered.AddListener(SetMatchPositionFalse);
        grabInteractable.selectExited.AddListener(SetMatchPositionTrue);
    }

    void Update()
    {
        // If the GameObject has a parent and matchPosition is true, sync its transform with the parent
        if (originalParent != null && matchPosition)
        {
            transform.position = originalParent.position;
            transform.rotation = originalParent.rotation;
        }
    }

    public void SetMatchPositionTrue(SelectExitEventArgs args)
    {
        // Reparent the gun to the original parent when released
        transform.SetParent(originalParent);
        matchPosition = true;

    }

    public void SetMatchPositionFalse(SelectEnterEventArgs args)
    {
        // Unparent the gun when picked up and allow the XR system to move it
        transform.SetParent(null);
        matchPosition = false;

    }

    void OnDestroy()
    {
        // Unsubscribe from events to avoid memory leaks
        grabInteractable.selectEntered.RemoveListener(SetMatchPositionFalse);
        grabInteractable.selectExited.RemoveListener(SetMatchPositionTrue);
    }
}
