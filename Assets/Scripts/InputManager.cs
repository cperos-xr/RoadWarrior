using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction brakeAction;
    private InputAction fireAction;

    // Events to broadcast input data
    public delegate void SteeringInputEvent(float value);
    public static event SteeringInputEvent OnSteer;

    public delegate void MovementInputEvent(float value);
    public static event MovementInputEvent OnMove;

    public delegate void BrakeInputEvent(float isBraking);
    public static event BrakeInputEvent OnBrake;

    public delegate void FireInputEvent(float isFiring);
    public static event FireInputEvent OnFire;

    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            moveAction = playerInput.actions.FindAction("Move");
            if (moveAction == null)
            {
                Debug.LogError("Move action not found.");
            }
            brakeAction = playerInput.actions.FindAction("Brake");
            if (brakeAction == null)
            {
                Debug.LogError("Brake action not found.");
            }
            fireAction = playerInput.actions.FindAction("Fire");
            if (fireAction == null)
            {
                Debug.LogError("Fire action not found.");
            }
        }
        else
        {
            Debug.LogError("PlayerInput component not found.");
        }
    }

    private void Update()
    {
        MovePlayer();
        BrakePlayer();
        FirePlayer();
    }

    private void MovePlayer()
    {
        if (moveAction == null) return;

        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Debug.Log("X " + moveValue.x);
        Debug.Log("Y " + moveValue.y);

        OnSteer?.Invoke(moveValue.x);
        OnMove?.Invoke(moveValue.y);
    }

    private void BrakePlayer()
    {
        if (brakeAction == null) return;

        float isBraking = brakeAction.ReadValue<float>();
        Debug.Log("Braking: " + isBraking);
        if (isBraking != 0)
        {
            OnBrake?.Invoke(isBraking);
        }

    }

    private void FirePlayer()
    {
        if (fireAction == null) return;

        float isFiring = fireAction.ReadValue<float>();
        Debug.Log("Firing: " + isFiring);
        OnFire?.Invoke(isFiring);
    }
}
