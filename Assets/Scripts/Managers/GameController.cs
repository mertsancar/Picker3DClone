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

            currentLevelIndex = PersistenceManager.GetCurrentLevelIndex();
            currentStageIndex = 0;
            
            levelManager.GenerateStartLevels(currentLevelIndex);
            currentLevel = levelManager.GetLevelById(currentLevelIndex);
            currentStage = currentLevel.GetStageById(currentStageIndex);
        }
        
        private void Start()
        {
            isPlaying = true;
            EventManager.instance.TriggerEvent(EventNames.GameStart);
        }
        
        private void OnGameStart()
        {
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

            var stageEndSeq = DOTween.Sequence();
            
            if (currentStage.IsBasketFull)
            {
                if (currentStageIndex == currentLevel.GetStageCount() - 1)
                {
                    stageEndSeq.AppendCallback(() => EventManager.instance.TriggerEvent(EventNames.LevelSuccess));
                    // return;
                }
                else
                {
                    stageEndSeq.AppendCallback(() => EventManager.instance.TriggerEvent(EventNames.StageSuccess));
                }
                stageEndSeq.AppendInterval(1f);
                
            }
            else
            {
                EventManager.instance.TriggerEvent(EventNames.StageFail);
                return;
            }


            stageEndSeq.AppendCallback(() =>
            {
                EventManager.instance.TriggerEvent(EventNames.StartMovement);
                isPlaying = true;
            });

        }

        private void OnStageSuccess()
        {
            currentStage.OnSuccess();
            levelManager.AddCompletedStage(currentStage);
            currentStageIndex++;
            currentStage = currentLevel.GetStageById(currentStageIndex);

            EventManager.instance.TriggerEvent(EventNames.ShowScreenRequested, typeof(StageSuccessScreen), null);
        }
        
        private void OnStageFail()
        {
            EventManager.instance.TriggerEvent(EventNames.ShowScreenRequested, typeof(StageFailScreen), null);
        }
        
        private void OnLevelSuccess()
        {
            //PersistenceManager.SetCurrentLevelIndex(currentLevelIndex);
            currentStage.OnSuccess();
            levelManager.AddCompletedStage(currentStage);
            currentStageIndex = 0;
            currentLevelIndex++;
            currentLevel = levelManager.GetLevelById(currentLevelIndex);
            currentStage = currentLevel.GetStageById(currentStageIndex);
            
            EventManager.instance.TriggerEvent(EventNames.ShowScreenRequested, typeof(LevelSuccessScreen), null);
        }
        
    }
    
}

