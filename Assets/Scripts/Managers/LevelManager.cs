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
        private int _totalLevelCount = 3;

        public void GenerateStartLevels(int levelNumber)
        {
            for (int i = levelNumber; i < levelNumber+generateLevelCountOnStart; i++)
            {
                GenerateLevelByNumber(i);
            }
            
        }
        
        private void GenerateLevelByNumber(int levelNumber)
        {
            var levelId = levelNumber <= _totalLevelCount ? levelNumber-1 : (levelNumber-1) % _totalLevelCount;
            var dataPath = System.IO.File.ReadAllText(Application.dataPath + "/Resources/Levels/Level" + levelId + ".json");
            LevelData data;
            
            try {
                data = JsonUtility.FromJson<LevelData>(dataPath);
            }
            catch {
                Debug.LogError("Error: The specified level " + levelId + " could not be found. The application has defaulted to the default level.");
                return;
            }

            data.levelNumber = levelNumber;

            if (_completedStages == null) _completedStages = new List<Stage>();
            if (_currentLevelsInScene == null) _currentLevelsInScene = new List<Level>();
            
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
            leveObject.name = "Level" + levelNumber;
            
            _currentLevelsInScene.Add(leveObject);
            
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
                    GenerateLevelByNumber(levels.GetChild(levels.childCount-1).GetComponent<Level>().levelNumber+1);
                    Destroy(levels.GetChild(0).gameObject);
                }
            }
        }
        
        public Level GetLevelByNumber(int levelNumber) 
        {
            for (int i = 0; i < levels.childCount; i++)
            {
                var level = levels.GetChild(i).GetComponent<Level>();
                if (level.levelNumber == levelNumber) return level;
            }

            return null;
        }
        
    }
    
}

