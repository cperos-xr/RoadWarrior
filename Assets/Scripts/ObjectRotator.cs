using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    // Enable or disable rotation for each axis
    public bool rotateX = false;
    public bool rotateY = false;
    public bool rotateZ = false;

    // Rotation speed for each axis
    public float speedX = 10f;
    public float speedY = 10f;
    public float speedZ = 10f;

    void Update()
    {
        // Calculate the rotation based on the enabled axes and their respective speeds
        float xRotation = rotateX ? speedX * Time.deltaTime : 0f;
        float yRotation = rotateY ? speedY * Time.deltaTime : 0f;
        float zRotation = rotateZ ? speedZ * Time.deltaTime : 0f;

        // Apply the rotation to the GameObject
        transform.Rotate(xRotation, yRotation, zRotation);
    }
}
