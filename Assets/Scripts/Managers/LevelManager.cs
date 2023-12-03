using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Levels;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public Transform levels;
        public Transform levelPrefab;
        public List<Stage> completedStages;
        public List<Level> currentLevelsInScene;
        public int lastGeneratedLevelIndex;
        public int totalStageCountInScene;

        public void GenerateStartLevels(int levelIndex)
        {
            for (int i = levelIndex; i < levelIndex+5; i++)
            {
                GenerateLevelByIndex(levelIndex);
            }
            lastGeneratedLevelIndex = levelIndex + 5;
            
            while (totalStageCountInScene < 9)
            {
                GenerateLevelByIndex(lastGeneratedLevelIndex+1);
            }
            
        }
        
        private void GenerateLevelByIndex(int levelId)
        {
            var dataPath = System.IO.File.ReadAllText(Application.dataPath + "/Resources/Levels/Level" + levelId%1 + ".json");
            LevelData data;
            
            try {
                data = JsonUtility.FromJson<LevelData>(dataPath);
            }
            catch {
                Debug.LogError("Error: The specified level " + levelId + " could not be found. The application has defaulted to the default level.");
                return;
            }
            
            totalStageCountInScene += data.stages.Count;

            Vector3 levelPosition;
            if (levels.childCount == 0)
            {
                levelPosition = new Vector3(0, 0, 0);
            }
            else
            {
                var lastLevel = levels.GetChild(levels.childCount - 1).GetComponent<Level>();
                var lastLevelStageCount = lastLevel.stages.childCount;
                var lastLevelLength = lastLevelStageCount * Stage.stageLength;
                levelPosition = new Vector3(0, 0, lastLevel.transform.position.z + lastLevelLength);
            }

            var leveObject = Instantiate(levelPrefab, levels).GetComponent<Level>();
            leveObject.Init(data);
            leveObject.transform.position = levelPosition;
            leveObject.name = "Level" + levelId;
            
            currentLevelsInScene.Add(leveObject);

            lastGeneratedLevelIndex = levelId;
            
        }

        private void Update()
        {
            if (completedStages.Count >= 2)
            {
                var completedStage = completedStages[0];
                completedStages.Remove(completedStage);
                GameController.instance.stagePoolManager.PushToPool(completedStage);
                GenerateLevelByIndex(lastGeneratedLevelIndex + 1);

            }
        }
        
        
    }
    
}

