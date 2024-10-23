using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(XROriginPlacer))]
public class XROriginPlacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector first
        DrawDefaultInspector();

        // Get a reference to the target component
        XROriginPlacer placer = (XROriginPlacer)target;

        // Add a button to the inspector
        if (GUILayout.Button("Place XR Origin"))
        {
            // Call the PlaceXROrigin method when the button is pressed
            placer.PlaceXROrigin();
        }
    }
}
