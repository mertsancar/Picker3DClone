using Managers;
using UnityEngine;

namespace Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform stages;
        private LevelData data;
        private int levelNumber;

        public void Init(int levelNumber, LevelData levelData)
        {
            data = levelData;
            this.levelNumber = levelNumber;
            
            for (int i = 0; i < data.stages.Count; i++)
            {
                var currentStageData = data.stages[i];
                var stage = GameController.Instance.stagePoolManager.PopFromPool();
                stage.transform.SetParent(stages);
                stage.Init(currentStageData);
                stage.transform.position = new Vector3(0, 0, Stage.stageLength * i);
            }
        }

        public LevelData GetLevelData()
        {
            return data;
        }
        
        public int GetLevelId()
        {
            return data.levelId;
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
            return data.stages.Count;
        }

    }
    
}

