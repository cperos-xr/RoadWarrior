using UnityEngine;

public class MatchParentTransform : MonoBehaviour
{
    public bool matchPosition = true;
    void Update()
    {
        // Check if the GameObject has a parent
        if (transform.parent != null && matchPosition)
        {
            // Match the position and rotation of the parent
            transform.position = transform.parent.position;
            transform.rotation = transform.parent.rotation;
        }
    }

    public void SetMatchPositionTrue()
    {
        matchPosition = true;
    }

    public void SetMatchPositionFalse()
    {
        matchPosition = false;
    }


}


