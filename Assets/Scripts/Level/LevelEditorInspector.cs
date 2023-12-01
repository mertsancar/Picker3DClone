using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// #if UNITY_EDITOR
[CustomEditor(typeof(LevelEditor))]
public class LevelEditorInspector : Editor
{
    private LevelEditor levelEditor;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (Application.isPlaying)
        {
            DrawPlayMode();
        }
        else
        {
            DrawEditMode();
        }
    }

    private void DrawEditMode()
    {
        levelEditor = target as LevelEditor;
        
        if (GUILayout.Button("Generate Level", GUILayout.Height(30), GUILayout.Width(100)))
        {
            Debug.Log("TempCounter: " + levelEditor.levelIndex);
        }
    }
    
    private void DrawPlayMode()
    {
        if (GUILayout.Button("Generate Level in Play Mode", GUILayout.Height(30), GUILayout.Width(100)))
        {
            Debug.Log("Level generated!!");
        }
    }

}
// #endif
