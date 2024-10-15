using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Rigidbody playerRigidBody;
    public Vector3 offset;
    public float speed = 2f;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 playerForward = (playerRigidBody.velocity + player.transform.forward).normalized;
        transform.position = Vector3.Lerp(transform.position, player.position + transform.TransformVector(offset) + playerForward * (-5f), speed * Time.deltaTime);
        transform.LookAt(player);
    }
}
