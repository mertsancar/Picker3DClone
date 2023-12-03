using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Character;
using Game.Collectables;
using Levels;
using UI;
using UnityEngine;

namespace Managers
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        
        public LevelManager levelManager;
        public StagePoolManager stagePoolManager;
        public CollectablePoolManager collectablePoolManager;

        public Character character;
        public Level currentLevel;
        public Stage currentStage;
        
        public int currentLevelIndex;
        public int currentStageIndex;

        public bool isPlaying;
        
        private void Awake()
        {
            instance = this;
            Application.targetFrameRate = 600;
            
            SetGameEvents();
            
            SetLevel();
            
        }
        
        private void SetGameEvents()
        {
            EventManager.instance.AddListener(EventNames.GameStart, OnGameStart);
            EventManager.instance.AddListener(EventNames.StageEnd, OnStageEnd);
            EventManager.instance.AddListener(EventNames.StageSuccess, OnStageSuccess);
            EventManager.instance.AddListener(EventNames.StageFail, OnStageFail);
            EventManager.instance.AddListener(EventNames.LevelSuccess, OnLevelSuccess);
        }
        private void SetLevel()
        {
            stagePoolManager.Init();
            collectablePoolManager.Init();
            
            levelManager.GenerateStartLevels(PersistenceManager.GetCurrentLevelIndex());
            currentLevel = levelManager.currentLevelsInScene[0];
        }
        
        private void Start()
        {
            isPlaying = true;
            EventManager.instance.TriggerEvent(EventNames.GameStart);
        }
        
        private void OnGameStart()
        {
            currentStageIndex = 0;
            EventManager.instance.TriggerEvent(EventNames.StartMovement);
        }

        private void Update()
        {
            OnGameContinue();
        }
        
        private void OnGameContinue()
        {
            
        }

        private void OnStageEnd()
        {
            isPlaying = false;
            
            currentStage = currentLevel.stages.GetChild(currentStageIndex).GetComponent<Stage>();
            if (currentStage.IsBasketFull)
            {
                EventManager.instance.TriggerEvent(EventNames.StageSuccess);
            }
            else
            {
                EventManager.instance.TriggerEvent(EventNames.StageFail);
            }
        }

        private void OnStageSuccess()
        {
            levelManager.completedStages.Add(currentStage); 
            
            var seq = DOTween.Sequence();
            seq.AppendCallback(currentStage.OnSuccess);
            seq.AppendCallback(() =>
            {
                currentStageIndex++;
                if (currentStageIndex > currentLevel.stages.childCount - 1)
                {
                    currentLevelIndex++;
                    currentStageIndex = 0;
                    currentLevel = levelManager.levels.GetChild(currentLevelIndex).GetComponent<Level>();
                }
            });
            seq.AppendCallback(() => EventManager.instance.TriggerEvent(EventNames.ShowScreenRequested, typeof(StageSuccessScreen), null));
            seq.AppendInterval(1f);
            seq.AppendCallback(() => EventManager.instance.TriggerEvent(EventNames.StartMovement));
            seq.OnComplete(() => isPlaying = true);
        }
        
        private void OnStageFail()
        {
            EventManager.instance.TriggerEvent(EventNames.ShowScreenRequested, typeof(StageFailScreen), null);
        }
        
        private void OnLevelSuccess()
        {
            EventManager.instance.TriggerEvent(EventNames.ShowScreenRequested, typeof(LevelSuccessScreen), null);
        }
        
    }
    
}

