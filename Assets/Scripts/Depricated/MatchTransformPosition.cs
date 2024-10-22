using UnityEngine;

public class MatchTransformPosition : MonoBehaviour
{
    public MountConfig mountConfig;

    public delegate void TransformPosition(Transform position, MountConfig mount);
    public static event TransformPosition OnTransformPosition;

    void Update()
    {
        OnTransformPosition?.Invoke(transform, mountConfig);
    }
}
