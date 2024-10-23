using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.XR.CoreUtils.XROrigin;


// a serializable enum to determine the player's position
[Serializable]
public enum PlayerPosition
{
    Driving,
    Gunner,
    ThirdPerson
}

public class PlayerPositionManager : MonoBehaviour
{
    public PlayerInputContols input;

    public XROrigin xrOrigin;

    public Transform playerDrivingPosition;
    public Transform playerGunnerPosition;
    public CameraController thirdPersonCameraController;

    public PlayerPosition playerPosition;

    // a delagate to broadcast when the player has changed positions
    public delegate void PlayerPositionChanged(PlayerPosition playerPosition);
    public static event PlayerPositionChanged OnPlayerPositionChanged;

    void Awake()
    {
        input = new PlayerInputContols();
    }

    private void OnEnable()
    {
        input.Enable();
        CreateDrivableCar.OnDrivableCarCreated += PlacePlayerInVehicle;
        input.Car.CyclePlayerPosition.performed += CyclePlayerPosition;

    }

    private void OnDisable()
    {
        input.Disable();
        CreateDrivableCar.OnDrivableCarCreated -= PlacePlayerInVehicle;
        input.Car.CyclePlayerPosition.performed -= CyclePlayerPosition;
    }

    private void CyclePlayerPosition(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            CyclePlayerPosition();
        }
    }

    public void CyclePlayerPosition()
    {
        switch (playerPosition)
        {
            case PlayerPosition.Driving:
                PlacePlayerOnGun();
                break;
            case PlayerPosition.Gunner:
                PlacePlayer3rdPerson();
                break;
            case PlayerPosition.ThirdPerson:
                PlacePlayerInVehicle();
                break;
        }
    }

    public void PlacePlayerInVehicle()
    {
        playerPosition = PlayerPosition.Driving;
        //PlacePlayerAtLocation(playerDrivingPosition);
        Recenter(playerDrivingPosition);
        thirdPersonCameraController.enabled = false;
        OnPlayerPositionChanged?.Invoke(playerPosition); // for material swap
    }

    public void Recenter(Transform recenterTransform)
    {
        xrOrigin.MoveCameraToWorldLocation(recenterTransform.position);
        xrOrigin.MatchOriginUpCameraForward(recenterTransform.up, recenterTransform.forward);
        xrOrigin.transform.SetParent(recenterTransform);
    }

    public void PlacePlayerOnGun()
    {
        playerPosition = PlayerPosition.Gunner;
        //PlacePlayerAtLocation(playerGunnerPosition);
        Recenter(playerGunnerPosition);
        thirdPersonCameraController.enabled = false;
        OnPlayerPositionChanged?.Invoke(playerPosition);
    }

    public void PlacePlayer3rdPerson()
    {
        playerPosition = PlayerPosition.ThirdPerson;
        thirdPersonCameraController.enabled = true;
        OnPlayerPositionChanged?.Invoke(playerPosition);
    }
}
