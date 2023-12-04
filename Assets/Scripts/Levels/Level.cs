﻿using System.Collections.Generic;
using Game.Collectables;
using Managers;
using UnityEditor;
using UnityEngine;

namespace Levels
{
    public class Level : MonoBehaviour
    {
        [HideInInspector] public int levelId;
        public Transform stages;

        public void Init(LevelData levelData)
        {
            levelId = levelData.levelId;

            for (int i = 0; i < levelData.stages.Count; i++)
            {
                var currentStageData = levelData.stages[i];
                var stage = GameController.instance.stagePoolManager.PopFromPool();
                stage.transform.SetParent(stages);
                stage.Init(currentStageData);
                stage.transform.position = new Vector3(0, 0, Stage.stageLength * i);
            }
        }

        public Stage GetStageById(int currentStageIndex)
        {
            return stages.GetChild(currentStageIndex).GetComponent<Stage>();
        }

        public int GetStageCount()
        {
            return stages.childCount;
        }
        
        public void InitForLevelEditor(LevelData levelData)
        {
            levelId = levelData.levelId;

            var stageObject = PrefabUtility.LoadPrefabContents("Assets/Prefabs/Levels/Stage/Stage.prefab");
            for (int i = 0; i < levelData.stages.Count; i++)
            {
                var currentStageData = levelData.stages[i];
                var stage = Instantiate(stageObject, stages).GetComponent<Stage>();
                stage.transform.position = new Vector3(0, 0, Stage.stageLength * i);
                stage.InitForEditor(currentStageData);
            }
        }

    }
    
}

