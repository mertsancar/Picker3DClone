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
        public int generateLevelCountOnStart;
        private List<Stage> _completedStages;
        private List<Level> _currentLevelsInScene;
        private int _lastGeneratedLevelIndex;
        private int _totalStageCountInScene;

        private void Awake()
        {

        }

        public void GenerateStartLevels(int levelIndex)
        {
            for (int i = levelIndex; i < levelIndex+generateLevelCountOnStart; i++)
            {
                GenerateLevelByIndex(levelIndex);
            }
            _lastGeneratedLevelIndex = levelIndex + 5;
            
            while (_totalStageCountInScene < 9)
            {
                GenerateLevelByIndex(_lastGeneratedLevelIndex+1);
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

            if (_completedStages == null) _completedStages = new List<Stage>();
            if (_currentLevelsInScene == null) _currentLevelsInScene = new List<Level>();
            
            _totalStageCountInScene += data.stages.Count;

            Vector3 levelPosition;
            if (levels.childCount == 0)
            {
                levelPosition = new Vector3(0, 0, 0);
            }
            else
            {
                var lastLevel = levels.GetChild(levels.childCount - 1).GetComponent<Level>();
                var lastLevelStageCount = lastLevel.GetStageCount();
                var lastLevelLength = lastLevelStageCount * Stage.stageLength;
                levelPosition = new Vector3(0, 0, lastLevel.transform.position.z + lastLevelLength);
            }

            var leveObject = Instantiate(levelPrefab, levels).GetComponent<Level>();
            leveObject.Init(data);
            leveObject.transform.position = levelPosition;
            leveObject.name = "Level" + levelId;
            
            _currentLevelsInScene.Add(leveObject);

            _lastGeneratedLevelIndex = levelId;
            
        }
        
        public void AddCompletedStage(Stage completedStage) 
        {
            _completedStages.Add(completedStage);
        }
        
        private void Update()
        {
            if (_completedStages.Count >= 4)
            {
                var completedStage = _completedStages[0];
                _completedStages.Remove(completedStage);
                GameController.instance.stagePoolManager.PushToPool(completedStage);
                if (GameController.instance.stagePoolManager.poolSize >= 3)
                {
                    GenerateLevelByIndex(_lastGeneratedLevelIndex + 1);
                }
            }
        }
        
        public Level GetLevelById(int levelIndex) 
        {
            return levels.GetChild(levelIndex).GetComponent<Level>();
        }
        
    }
    
}

