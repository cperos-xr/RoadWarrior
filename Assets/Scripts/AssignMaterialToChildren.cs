using UnityEngine;

[ExecuteInEditMode]
public class AssignMaterialToChildren : MonoBehaviour
{
    public Material materialToAssign;

    public void AssignMaterial()
    {
        if (materialToAssign == null)
        {
            Debug.LogWarning("No material assigned. Please assign a material in the inspector.");
            return;
        }

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.sharedMaterial = materialToAssign;
        }

        Debug.Log("Material assigned to all child objects.");
    }
}
