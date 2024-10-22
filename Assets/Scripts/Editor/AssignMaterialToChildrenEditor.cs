using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AssignMaterialToChildren))]
public class AssignMaterialToChildrenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AssignMaterialToChildren script = (AssignMaterialToChildren)target;

        if (GUILayout.Button("Assign Material to Children"))
        {
            script.AssignMaterial();
        }
    }
}
