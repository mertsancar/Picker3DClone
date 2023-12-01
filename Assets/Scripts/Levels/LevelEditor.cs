using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Levels
{
    public class LevelEditor : MonoBehaviour
    {
        public Level level;
        
        public int levelId;
        
        public void AddStage()
        {
            Debug.Log("Add Stage");

            var prefabPath = "Assets/Prefabs/Levels/Stage.prefab";
            var stagePrefab = PrefabUtility.LoadPrefabContents(prefabPath);
            var stageObject = Instantiate(stagePrefab, level.stages).GetComponent<Stage>();

            var zPosition = (level.stages.childCount-1) * 24.97f;

            stageObject.transform.position = new Vector3(0, 0, zPosition);
            
            Selection.objects = new Object[] { stageObject.gameObject };

        }
        
        public void GetLevel()
        {
            Debug.Log("Get Level");
            
            var prefabPath = "Assets/Resources/Levels/Prefabs/level" + levelId + ".prefab";
            Level newLevel;
            
            try
            {
                newLevel = PrefabUtility.LoadPrefabContents(prefabPath).GetComponent<Level>();
            }
            catch (Exception e)
            {
                Debug.LogError("Error: The specified level " + levelId + " could not be found. The application has defaulted to the default level.");

                ClearLevel();
                
                return;
            }

            DestroyImmediate(level.gameObject);
            Instantiate(newLevel, transform);

            level = transform.GetChild(0).GetComponent<Level>();
        }
        
        
        public void SaveLevel()
        {
            Debug.Log("Save Level");

            level.levelId = levelId;
            
            var prefabPath = "Assets/Resources/Levels/Prefabs/level" + levelId + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(level.gameObject, prefabPath);
        }
        
        public void ClearLevel()
        {
            Debug.Log("Save Level");

            var prefabPath = "Assets/Prefabs/Levels/Level.prefab";
            var defaultLevel = PrefabUtility.LoadPrefabContents(prefabPath).GetComponent<Level>();
            DestroyImmediate(level.gameObject);
            Instantiate(defaultLevel, transform);
            
            level = transform.GetChild(0).GetComponent<Level>();
        }


    }
    
}

