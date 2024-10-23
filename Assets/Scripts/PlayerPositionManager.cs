using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
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
    private float yoffset;

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

    void PlacePlayerAtLocation(Transform location)
    {
        yoffset = location.position.y;
        Debug.Log("Offset should be:" + yoffset);
        xrOrigin.RequestedTrackingOriginMode = TrackingOriginMode.Device;
        xrOrigin.CameraFloorOffsetObject = location.gameObject;
        xrOrigin.CameraYOffset = yoffset;

        Recenter(location);

        xrOrigin.transform.SetParent(location);
        xrOrigin.transform.position = location.position;
        xrOrigin.transform.rotation = location.rotation;
    }

    public void PlacePlayerInVehicle()
    {
        playerPosition = PlayerPosition.Driving;
        PlacePlayerAtLocation(playerDrivingPosition);
        //yoffset = playerDrivingPosition.position.y;
        //Debug.Log("Offset should be:" + yoffset);
        //xrOrigin.RequestedTrackingOriginMode = TrackingOriginMode.Device;
        //xrOrigin.CameraFloorOffsetObject = playerDrivingPosition.gameObject;
        //xrOrigin.CameraYOffset = yoffset;
        ////recenter the xr origin
        //Recenter(playerDrivingPosition);


        //xrOrigin.transform.SetParent(playerDrivingPosition);
        //xrOrigin.transform.position = playerDrivingPosition.position;
        //xrOrigin.transform.rotation = playerDrivingPosition.rotation;
        thirdPersonCameraController.enabled = false;
        OnPlayerPositionChanged?.Invoke(playerPosition); // for material swap
    }

    public void Recenter(Transform recenterTransform)
    {
        // Calculate the offset to move the XR Origin so that the camera aligns with the recenterTransform
        Vector3 offset = xrOrigin.Camera.transform.position - xrOrigin.Origin.transform.position;
        Vector3 targetPosition = recenterTransform.position - offset;

        // Move the XR Origin to the new target position
        xrOrigin.MoveCameraToWorldLocation(targetPosition);
    }

    public void PlacePlayerOnGun()
    {
        playerPosition = PlayerPosition.Gunner;
        //xrOrigin.RequestedTrackingOriginMode = TrackingOriginMode.Device;
        PlacePlayerAtLocation(playerGunnerPosition);
        //xrOrigin.CameraFloorOffsetObject = playerGunnerPosition.gameObject;
        //xrOrigin.CameraYOffset = 0.0f;
        //playerPosition = PlayerPosition.Gunner;
        //xrOrigin.transform.SetParent(playerGunnerPosition);
        //xrOrigin.transform.position = playerGunnerPosition.position;
        //xrOrigin.transform.rotation = playerGunnerPosition.rotation;
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
