using UnityEditor;
using UnityEngine;

// #if UNITY_EDITOR
namespace Levels
{
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
            
            GUILayout.Space(30f);
            
            if (GUILayout.Button("Create Level", GUILayout.Height(40), GUILayout.Width(100)))
            {
                levelEditor.CreateLevel();
            }
            
            if (GUILayout.Button("Get Level", GUILayout.Height(40), GUILayout.Width(100)))
            {
                levelEditor.GetLevel();
            }
            
            if (GUILayout.Button("Save Level", GUILayout.Height(40), GUILayout.Width(100)))
            {
                levelEditor.SaveLevel();
            }
            
            GUILayout.Space(30f);
            
            if (GUILayout.Button("Add Stage", GUILayout.Height(30), GUILayout.Width(100)))
            {
                levelEditor.AddStage();
            }
            
            GUILayout.Space(60f);
            
            if (GUILayout.Button("Clear Level", GUILayout.Height(40), GUILayout.Width(100)))
            {
                levelEditor.ClearLevel();
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
    
}

