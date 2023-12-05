﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Levels
{
    [CustomEditor(typeof(StageEditor))]
    public class StageEditorInspector : Editor
    {
        private StageEditor stageEditor;
        
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
            stageEditor = target as StageEditor;
            
            GUILayout.Space(30);
            
            if (GUILayout.Button("Add Cube Collectable", GUILayout.Height(30), GUILayout.Width(150)))
            {
                stageEditor.AddCubeCollectable();
            }
            
            if (GUILayout.Button("Add Sphere Collectable", GUILayout.Height(30), GUILayout.Width(150)))
            {
                stageEditor.AddSphereCollectable();
            }
            
            if (GUILayout.Button("Add Capsule Collectable", GUILayout.Height(30), GUILayout.Width(150)))
            {
                stageEditor.AddCapsuleCollectable();
            }
            
            GUILayout.Space(45);
            
            if (GUILayout.Button("Delete last collectable", GUILayout.Height(30), GUILayout.Width(150)))
            {
                stageEditor.DeleteLastCollectable();
            }
            
            GUILayout.Space(30);
            
            GUILayout.Label("NOTE: You can save all your works using 'Save Level' button in 'LevelEditor' object.");
            
        }
        
        private void DrawPlayMode()
        {
            if (GUILayout.Button("Generate Level in Play Mode", GUILayout.Height(30), GUILayout.Width(100)))
            {
                Debug.Log("Level generated!!");
            }
        }
    }
    
}
