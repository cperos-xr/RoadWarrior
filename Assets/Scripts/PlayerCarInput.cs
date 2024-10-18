using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCarInput : MonoBehaviour
{
    public PlayerInputContols input;
    public float throttleInput;
    public float throttleDampened;
    public float steeringInput;
    public float steeringDampened;
    public float brakeInput;

    public float steeringDampenSpeed = 5;
    public float throttleDampenSpeed = 10;

    // Start is called before the first frame update
    void Awake()
    {
        input = new PlayerInputContols();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Car.Throttle.performed += ApplyThrottle;
        input.Car.Throttle.canceled += ReleaseThrottle;
        input.Car.Steering.performed += ApplySteering;
        input.Car.Steering.canceled += ReleaseSteering;
        input.Car.Brake.performed += ApplyBrake;
        input.Car.Brake.canceled += ReleaseBrake;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Car.Throttle.performed -= ApplyThrottle;
        input.Car.Throttle.canceled -= ReleaseThrottle;
        input.Car.Steering.performed -= ApplySteering;
        input.Car.Steering.canceled -= ReleaseSteering;
        input.Car.Brake.performed -= ApplyBrake;
        input.Car.Brake.canceled -= ReleaseBrake;
    }

    private void Update()
    {
        throttleDampened = DampendInput(throttleInput, throttleDampened, throttleDampenSpeed);
        steeringDampened = DampendInput(steeringInput, steeringDampened, steeringDampenSpeed);
    }

    private float DampendInput(float input, float output, float dampenSpeed)
    {
        return Mathf.Lerp(output, input, Time.deltaTime * dampenSpeed);
    }

    private void ApplyThrottle(InputAction.CallbackContext value)
    {
        throttleInput = value.ReadValue<float>();
    }

    private void ReleaseThrottle(InputAction.CallbackContext value)
    {
        throttleInput = 0;
    }

    private void ApplySteering(InputAction.CallbackContext value)
    {
        steeringInput = value.ReadValue<float>();
    }

    private void ReleaseSteering(InputAction.CallbackContext value)
    {
        steeringInput = 0;
    }

    private void ApplyBrake(InputAction.CallbackContext value)
    {
        brakeInput = value.ReadValue<float>();
    }

    private void ReleaseBrake(InputAction.CallbackContext value)
    {
        brakeInput = 0;
    }
}
