using System;
using System.Collections.Generic;
using Game.Collectables;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Levels
{
    public class LevelEditor : MonoBehaviour
    {
        public Level level;
        public Transform levelPrefab;
        public int levelId;

        public void CreateLevel()
        {
            Debug.Log("Default Level Created!");
            var levelObject = Instantiate(levelPrefab, transform).GetComponent<Level>();
            level = levelObject;
        }
        
        public void AddStage()
        {
            Debug.Log("Stage Added!");

            var prefabPath = "Assets/Prefabs/Levels/Stage/Stage.prefab";
            var stagePrefab = PrefabUtility.LoadPrefabContents(prefabPath);
            var stageObject = Instantiate(stagePrefab, level.stages).GetComponent<Stage>();

            var zPosition = (level.GetStageCount()-1) * 24.97f;

            stageObject.transform.position = new Vector3(0, 0, zPosition);
            
            Selection.objects = new Object[] { stageObject.gameObject };

        }
        
        public void GetLevel()
        {
            Debug.Log("Get Level");
            
            ClearLevel();
            
            var dataPath = System.IO.File.ReadAllText(Application.dataPath + "/Resources/Levels/Level" + levelId + ".json");
            LevelData data;
            
            try {
                data = JsonUtility.FromJson<LevelData>(dataPath);
            }
            catch {
                Debug.LogError("Error: The specified level " + levelId + " could not be found. The application has defaulted to the default level.");
                CreateLevel();
                return;
            }

            levelId = data.levelId;
            
            var leveObject = Instantiate(levelPrefab, transform).GetComponent<Level>();
            leveObject.InitForLevelEditor(data);
            leveObject.name = "Level" + levelId;
            
            level = leveObject;
        }
        
        
        public void SaveLevel()
        {
            Debug.Log("Save Level");
            
            var levelStages = new List<StageData>();
            for (var i = 0; i < level.GetStageCount(); i++)
            {
                var currentStage = level.GetStageByIndex(i);
                
                var collectables = new List<CollectableItemData>();
                foreach (var collectable in currentStage.collectables.CollectableList)
                {
                    collectables.Add(new CollectableItemData
                    {
                        position = collectable.transform.localPosition,
                        rotation =  collectable.transform.localRotation,
                        scale = collectable.transform.localScale,
                        type = collectable.type
                    });
                }
                
                levelStages.Add(new StageData
                {
                    basketCapacity = currentStage.basketCapacity,
                    collectables = collectables
                });
            }

            var levelData = new LevelData
            {
                levelId = levelId,
                stages = levelStages
            };
            
            var data = JsonUtility.ToJson(levelData);
            System.IO.File.WriteAllText(Application.dataPath + "/Resources/Levels/Level" + levelData.levelId + ".json", data);
        }
        
        public void ClearLevel()
        {
            Debug.Log("Level clear!");

            if (level != null) DestroyImmediate(level.gameObject);

        }


    }
    
}

