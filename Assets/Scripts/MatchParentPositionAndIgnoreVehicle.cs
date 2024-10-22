using System.Collections.Generic;
using UnityEngine;

public class MatchParentPositionAndIgnoreVehicle : MonoBehaviour
{
    public bool matchparentPosition = true;

    public List <Collider> objectColliders;
    public Collider vehicleCollider;


    private void Update()
    {
        SetPositionToParent();
    }

    private void SetPositionToParent()
    {
        if (matchparentPosition)
        {
            transform.position = transform.parent.position;
            transform.rotation = transform.parent.rotation;
        }
    }

    public void AttachToVehicle()
    {
        matchparentPosition = true;
        IgnoreCollisionWithVehicle(false);
    }

    public void DetachFromVehicle()
    {
        matchparentPosition = false;
        IgnoreCollisionWithVehicle(true);
    }

    void IgnoreCollisionWithVehicle(bool ignore)
    {
        foreach (Collider objectCollider in objectColliders)
        {
            Physics.IgnoreCollision(objectCollider, vehicleCollider, ignore);

        }
    }



}
