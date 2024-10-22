using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwap : MonoBehaviour
{

    public Material originalMaterial;
    public Material newMaterial;
    // Start is called before the first frame update
    void Start()
    {
        // set the original material to the current material of the object
        originalMaterial = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        PlayerPositionManager.OnPlayerPositionChanged += OnPlayerPositionChanged;
    }

    private void OnDisable()
    {
        PlayerPositionManager.OnPlayerPositionChanged -= OnPlayerPositionChanged;
    }

    private void OnPlayerPositionChanged(PlayerPosition playerPosition)
    {
        //switch case to determine the player's position
        switch (playerPosition)
        {
            case PlayerPosition.Driving:
                SetToNewMaterial();
                break;
            case PlayerPosition.Gunner:
                SetToOriginalMaterial();
                break;
            case PlayerPosition.ThirdPerson:
                SetToOriginalMaterial();
                break;
        }
    }

    private void SetToNewMaterial()
    {
        // set the material of the object to the new material
        GetComponent<Renderer>().material = newMaterial;
    }

    private void SetToOriginalMaterial()
    {
        // set the material of the object to the original material
        GetComponent<Renderer>().material = originalMaterial;
    }
}
