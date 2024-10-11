using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;

    // Events to broadcast input data
    public delegate void SteeringInputEvent(float value);
    public static event SteeringInputEvent OnSteer;

    public delegate void MovementInputEvent(float value);
    public static event MovementInputEvent OnMove;

    public delegate void BrakeInputEvent(bool isBraking);
    public static event BrakeInputEvent OnBrake;

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
        }
        else
        {
            Debug.LogError("PlayerInput component not found.");
        }
    }

    private void Update()
    {
        MovePlayer();
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
}
