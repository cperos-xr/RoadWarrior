using UnityEngine;

public class Projectile : MonoBehaviour
{
    // The speed at which the projectile will move forward
    public float speed = 20f;

    // Update is called once per frame
    void Update()
    {
        // Move the projectile forward (relative to its current rotation)
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Optional: Destroy the projectile after a certain amount of time (e.g., 5 seconds)
    void Start()
    {
        Destroy(gameObject, 5f); // Change 5f to any value you want for the projectile's lifetime
    }
}
