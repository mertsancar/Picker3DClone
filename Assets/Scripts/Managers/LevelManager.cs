using UnityEngine;
using System.Collections.Generic;
using Levels;
using UnityEngine.SocialPlatforms;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform levels;
        [SerializeField] private Transform levelPrefab;
        public int generateLevelCountOnStart;
        private List<Stage> _completedStages;
        private List<Level> _currentLevelsInScene;
        private readonly int _totalLevelCount = 11;

        public void GenerateStartLevels(int levelNumber)
        {
            for (int i = levelNumber; i < levelNumber+generateLevelCountOnStart; i++)
            {
                GenerateLevelByNumber(i);
            }
        }
        
        public Level GetLevelByNumber(int levelNumber) 
        {
            for (int i = 0; i < levels.childCount; i++)
            {
                var level = levels.GetChild(i).GetComponent<Level>();
                if (level.GetLevelNumber() == levelNumber) return level;
            }

            return null;
        }
        
        public int GetLevelIdByNumber(int levelNumber) 
        {
            return levelNumber <= _totalLevelCount ? levelNumber-1 : Random.Range(0, _totalLevelCount); // for sorted order: (levelNumber-1) % _totalLevelCount 
        }
        
        public void AddCompletedStage(Stage completedStage) 
        {
            _completedStages.Add(completedStage);
        }
        
        private void GenerateLevelByNumber(int levelNumber)
        {
            var levelId = GetLevelIdByNumber(levelNumber);
            
            var data = LevelParser.GetLevelDataById(levelId);
            
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
            leveObject.Init(levelNumber, data);
            leveObject.transform.position = levelPosition;
            leveObject.name = "Level" + levelNumber;
            
            _currentLevelsInScene.Add(leveObject);
        }
        
        private void Update()
        {
            if (_completedStages.Count >= 4)
            {
                var completedStage = _completedStages[0];
                _completedStages.Remove(completedStage);
                GameController.Instance.stagePoolManager.PushToPool(completedStage);
                if (GameController.Instance.stagePoolManager.poolSize >= 3)
                {
                    var levelNumber = levels.GetChild(levels.childCount - 1).GetComponent<Level>().GetLevelNumber();
                    GenerateLevelByNumber(levelNumber+1);
                    Destroy(levels.GetChild(0).gameObject);
                }
            }
        }
        
    }
    
}

