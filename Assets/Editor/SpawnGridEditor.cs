using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnGrid))]
public class SpawnGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpawnGrid grid = (SpawnGrid)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Generate Spawn Points"))
        {
            grid.Generate();
        }

        if (GUILayout.Button("Clear Spawn Points"))
        {
            grid.Clear();
        }
    }
}
