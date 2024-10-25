using UnityEngine;

public class BrakingZone : MonoBehaviour
{
    public float recommendedMaxSpeed;
    private void OnTriggerEnter(Collider other)
    {
        AiCarController car = other.GetComponent<AiCarController>();
        if (car)
        {
            car.isInsideBraking = true;
            car.recommendedMaxSpeed = recommendedMaxSpeed;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        AiCarController car = other.GetComponent<AiCarController>();
        if (car)
        {
            car.isInsideBraking = false;
            car.recommendedMaxSpeed = car.maximumSpeed;
        }
    }
}