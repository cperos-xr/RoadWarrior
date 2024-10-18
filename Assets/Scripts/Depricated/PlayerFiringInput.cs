using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFiringInput : MonoBehaviour
{
    public PlayerInputContols input;
    public float fireInput;


    void Awake()
    {
        input = new PlayerInputContols();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Car.Fire.performed += ApplyFire;
        input.Car.Fire.canceled += ReleaseFire;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Car.Fire.performed -= ApplyFire;
        input.Car.Fire.canceled -= ReleaseFire;
    }

    private void ApplyFire(InputAction.CallbackContext value)
    {
        fireInput = value.ReadValue<float>();
    }

    private void ReleaseFire(InputAction.CallbackContext value)
    {
        fireInput = 0;
    }
}
