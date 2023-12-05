using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Levels
{
    public class LevelEditor : MonoBehaviour
    {
        [SerializeField] private Transform levelPrefab;
        [SerializeField] private int levelId;
        private Level _level;
        private Transform _stages;

        private void InitForLevelEditor(LevelData levelData)
        {
            levelId = levelData.levelId;

            var stageObject = PrefabUtility.LoadPrefabContents(LevelParser.StagePrefabPath);
            for (int i = 0; i < levelData.stages.Count; i++)
            {
                var currentStageData = levelData.stages[i];
                var stagEditor = Instantiate(stageObject, _stages).GetComponent<StageEditor>();
                stagEditor.transform.position = new Vector3(0, 0, Stage.stageLength * i);
                stagEditor.InitForEditor(currentStageData);
            }
        }
        
        public void CreateLevel()
        {
            Debug.Log("Default Level Created");
            var levelObject = Instantiate(levelPrefab, transform).GetComponent<Level>();
            _level = levelObject;
            _stages = levelObject.transform.GetChild(0);
            
            AddStage();
        }
        
        public void AddStage()
        {
            Debug.Log("Stage Added");

            var prefabPath = LevelParser.StagePrefabPath;
            var stagePrefab = PrefabUtility.LoadPrefabContents(prefabPath);
            var stageObject = Instantiate(stagePrefab, _level.transform.GetChild(0)).GetComponent<Stage>();

            var zPosition = (_level.GetStageCount()-1) * 24.97f;

            stageObject.transform.position = new Vector3(0, 0, zPosition);
            
            Selection.objects = new Object[] { stageObject.gameObject };

        }
        
        public void GetLevel()
        {
            Debug.Log("Get Level");
            
            ClearLevel();
            
            var data = LevelParser.GetLevelDataById(levelId);
            if (data.levelId == -1)
            {
                return;
            }

            levelId = data.levelId;
            
            var levelObject = Instantiate(levelPrefab, transform).GetComponent<Level>();
            levelObject.name = "Level" + levelId;
            _level = levelObject;
            _stages = levelObject.transform.GetChild(0);
            InitForLevelEditor(data);
            
        }
        
        
        public void SaveLevel()
        {
            Debug.Log("Level saved");
            
            var levelStages = new List<StageData>();
            for (var i = 0; i < _level.GetStageCount(); i++)
            {
                var currentStagEditor = _level.GetStageByIndex(i).GetComponent<StageEditor>();
                
                var collectables = new List<CollectableItemData>();
                var x = currentStagEditor.GetCollectables();
                foreach (var collectable in x)
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
                    basketCapacity = currentStagEditor.basketCapacity,
                    collectables = collectables
                });
            }

            var levelData = new LevelData
            {
                levelId = levelId,
                stages = levelStages
            };
            
            LevelParser.SetLevelDataById(levelData);
        }
        
        public void ClearLevel()
        {
            Debug.Log("Level clear");

            if (_level != null) DestroyImmediate(_level.gameObject);

        }
    }
    
}

