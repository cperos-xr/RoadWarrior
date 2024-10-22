using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ThumbstickInput : MonoBehaviour
{
    public PlayerInputContols input;
    public TextMeshProUGUI thumbstickValueText;

    private Vector2 thumbstickInput;

    void Awake()
    {
        input = new PlayerInputContols();
    }

    private void OnEnable()
    {
        input.Enable();
        input.XRController.ThumbstickRight.performed += OnThumbstickMove;
        input.XRController.ThumbstickRight.canceled += OnThumbstickRelease;
    }

    private void OnDisable()
    {
        input.Disable();
        input.XRController.ThumbstickRight.performed -= OnThumbstickMove;
        input.XRController.ThumbstickRight.canceled -= OnThumbstickRelease;
    }

    private void Update()
    {
        // Update the TMP text with the current thumbstick input values
        thumbstickValueText.text = $"X: {thumbstickInput.x:F2}, Y: {thumbstickInput.y:F2}";
    }

    private void OnThumbstickMove(InputAction.CallbackContext context)
    {
        thumbstickInput = context.ReadValue<Vector2>();
    }

    private void OnThumbstickRelease(InputAction.CallbackContext context)
    {
        thumbstickInput = Vector2.zero;
    }
}
