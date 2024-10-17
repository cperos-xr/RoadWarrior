using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPositionController : MonoBehaviour
{
    public MountConfig frontLeft;
    public MountConfig frontRight;
    public MountConfig rearLeft;
    public MountConfig rearRight;

    public WheelSet currentWheelSet;

    public WheelTransforms wheelPositions;
    public WheelColliders wheelColliders;

    [SerializeField] CarController2 carController;



    private void OnEnable()
    {
        CreatedCarManager.OnWheelsSelected += OnWheelsSelected;
        WeaponMountPoint.OnChangeMountPoint += SetMyPosition;
    }

    private void OnDisable()
    {
        CreatedCarManager.OnWheelsSelected -= OnWheelsSelected;
        WeaponMountPoint.OnChangeMountPoint -= SetMyPosition;
    }

    private void OnWheelsSelected(WheelSet wheelSet)
    {
        Debug.Log("Wheel set selected: " + wheelSet.name);
        currentWheelSet = wheelSet;
        SetWheelPositions();
        SetColliderPositions();
        carController.wheelTransforms = currentWheelSet.wheelTransforms;
    }

    private void SetColliderPositions() 
    {
        if (wheelColliders.frontLeftWheelCollider != null && wheelPositions.frontLeftWheelTransform != null)
        {
            wheelColliders.frontLeftWheelCollider.transform.position = wheelPositions.frontLeftWheelTransform.position;
        }
        if (wheelColliders.frontRightWheelCollider != null && wheelPositions.frontRightWheelTransform != null)
        {
            wheelColliders.frontRightWheelCollider.transform.position = wheelPositions.frontRightWheelTransform.position;
        }
        if (wheelColliders.rearLeftWheelCollider != null && wheelPositions.rearLeftWheelTransform != null)
        {
            wheelColliders.rearLeftWheelCollider.transform.position = wheelPositions.rearLeftWheelTransform.position;
        }
        if (wheelColliders.rearRightWheelCollider != null && wheelPositions.rearRightWheelTransform != null)
        {
            wheelColliders.rearRightWheelCollider.transform.position = wheelPositions.rearRightWheelTransform.position;
        }
    }

    private void SetWheelPositions()
    {
        if (currentWheelSet != null)
        {
            if (wheelPositions.frontLeftWheelTransform != null)
            {
                currentWheelSet.wheelTransforms.frontLeftWheelTransform.position = wheelPositions.frontLeftWheelTransform.position;
            }
            if (wheelPositions.frontRightWheelTransform != null)
            {
                currentWheelSet.wheelTransforms.frontRightWheelTransform.position = wheelPositions.frontRightWheelTransform.position;
            }
            if (wheelPositions.rearLeftWheelTransform != null)
            {
                currentWheelSet.wheelTransforms.rearLeftWheelTransform.position = wheelPositions.rearLeftWheelTransform.position;
            }
            if (wheelPositions.rearRightWheelTransform != null)
            {
                currentWheelSet.wheelTransforms.rearRightWheelTransform.position = wheelPositions.rearRightWheelTransform.position;
            }
        }
    }

    private void SetMyPosition(GameObject myMountPoint, MountConfig mountConfig)
    {

        if (mountConfig == frontLeft)
        {
            wheelPositions.frontLeftWheelTransform = myMountPoint.transform;
            wheelColliders.frontLeftWheelCollider.transform.position = myMountPoint.transform.position;
            if (currentWheelSet != null)
            {
                currentWheelSet.wheelTransforms.frontLeftWheelTransform.position = myMountPoint.transform.position;
            }
        }
        else if (mountConfig == frontRight)
        {
            wheelPositions.frontRightWheelTransform = myMountPoint.transform;
            wheelColliders.frontRightWheelCollider.transform.position = myMountPoint.transform.position;
            if (currentWheelSet != null)
            {
                currentWheelSet.wheelTransforms.frontRightWheelTransform.position = myMountPoint.transform.position;
            }
        }
        else if (mountConfig == rearLeft)
        {
            wheelPositions.rearLeftWheelTransform = myMountPoint.transform;
            wheelColliders.rearLeftWheelCollider.transform.position = myMountPoint.transform.position;
            if (currentWheelSet != null)
            {
                currentWheelSet.wheelTransforms.rearLeftWheelTransform.position = myMountPoint.transform.position;
            }
        }
        else if (mountConfig == rearRight)
        {
            wheelPositions.rearRightWheelTransform = myMountPoint.transform;
            wheelColliders.rearRightWheelCollider.transform.position = myMountPoint.transform.position;
            if (currentWheelSet != null)
            {
                currentWheelSet.wheelTransforms.rearRightWheelTransform.position = myMountPoint.transform.position;
            }

        }
        
    }
}
