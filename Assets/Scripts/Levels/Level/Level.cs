using Managers;
using UnityEngine;

namespace Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform stages;
        private int levelNumber;
        private int levelId;

        public void Init(int levelNumber, LevelData levelData)
        {
            levelId = levelData.levelId;
            this.levelNumber = levelNumber;
            
            for (int i = 0; i < levelData.stages.Count; i++)
            {
                var currentStageData = levelData.stages[i];
                var stage = GameController.Instance.stagePoolManager.PopFromPool();
                stage.transform.SetParent(stages);
                stage.Init(currentStageData);
                stage.transform.position = new Vector3(0, 0, Stage.stageLength * i);
            }
        }

        public int GetLevelId()
        {
            return levelNumber;
        }
        
        public int GetLevelNumber()
        {
            return levelNumber;
        }
        
        public Stage GetStageByIndex(int currentStageIndex)
        {
            return stages.GetChild(currentStageIndex).GetComponent<Stage>();
        }

        public int GetStageCount()
        {
            return stages.childCount;
        }

    }
    
}

