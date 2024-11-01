using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRTurretInteractable))]
public class TurretRotator : MonoBehaviour
{
    [Header("Position Tracking Settings")]
    public bool trackPosition = true;
    public bool invertPositionX = false;
    public bool invertPositionY = false;
    public float positionSensitivity = 1.0f; // Controls position tracking sensitivity

    [Header("Rotation Tracking Settings")]
    public bool trackRotation = true;
    public bool invertRotationX = false;
    public bool invertRotationY = false;
    public float rotationSensitivity = 1.0f; // Controls rotation tracking sensitivity

    private XRTurretInteractable turretInteractable;
    private Transform selectingController;
    private Vector3 initialControllerLocalPosition;
    private Quaternion initialControllerLocalRotation;
    private Quaternion initialTurretRotation;

    private void Awake()
    {
        turretInteractable = GetComponent<XRTurretInteractable>();
        turretInteractable.selectEntered.AddListener(OnSelectEntered);
        turretInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDestroy()
    {
        turretInteractable.selectEntered.RemoveListener(OnSelectEntered);
        turretInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        selectingController = args.interactorObject.transform;
        initialControllerLocalPosition = selectingController.localPosition;
        initialControllerLocalRotation = selectingController.localRotation;
        initialTurretRotation = transform.localRotation;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Reset rotation on deselection
        transform.localRotation = initialTurretRotation;
        selectingController = null;
    }

    private void FixedUpdate()
    {
        if (selectingController == null) return;

        // Position-based rotation (left/right, up/down movement) using local position delta
        Vector3 localPositionDelta = selectingController.localPosition - initialControllerLocalPosition;
        float positionXRotation = (invertPositionX ? -localPositionDelta.y : localPositionDelta.y) * positionSensitivity;
        float positionYRotation = (invertPositionY ? -localPositionDelta.x : localPositionDelta.x) * positionSensitivity;

        // Rotation-based rotation (controller tilt left/right, up/down) using local rotation delta
        Quaternion localRotationDelta = selectingController.localRotation * Quaternion.Inverse(initialControllerLocalRotation);
        localRotationDelta.ToAngleAxis(out float angle, out Vector3 axis);

        // Convert angle-axis to euler angles for sensitivity adjustment and inversion
        Vector3 localRotationEuler = angle * axis;
        float rotationXRotation = (invertRotationX ? -localRotationEuler.x : localRotationEuler.x) * rotationSensitivity;
        float rotationYRotation = (invertRotationY ? -localRotationEuler.y : localRotationEuler.y) * rotationSensitivity;

        // Determine the final X and Y rotations based on the tracking settings
        float finalXRotation = 0;
        float finalYRotation = 0;

        if (trackPosition)
        {
            finalXRotation += positionXRotation;
            finalYRotation += positionYRotation;
        }

        if (trackRotation)
        {
            finalXRotation += rotationXRotation;
            finalYRotation += rotationYRotation;
        }

        // Apply the combined rotation to the object
        transform.localRotation = initialTurretRotation * Quaternion.Euler(finalXRotation, finalYRotation, 0);
    }
}
