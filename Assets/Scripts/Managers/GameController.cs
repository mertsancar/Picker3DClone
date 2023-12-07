using System;
using UnityEngine;
using Game.Character;
using Game.Collectables;
using Levels;
using UI;
using DG.Tweening;
using UI.Screens;

namespace Managers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        
        public LevelManager levelManager;
        public StagePoolManager stagePoolManager;
        public CollectablePoolManager collectablePoolManager;

        public Character character;
        public Level currentLevel;
        public Stage currentStage;
        
        public int currentLevelNumber;
        public int currentLevelId;
        public int currentStageIndex;

        public bool isPlaying;
        
        private void Awake()
        {
            Instance = this;
            Application.targetFrameRate = 600;
            
            SetLevel();
            
            SetGameEvents();
        }

        private void Start()
        {
            EventManager.instance.TriggerEvent(EventNames.ShowScreenRequested, typeof(MenuScreen), null);
        }

        private void SetGameEvents()
        {
            EventManager.instance.AddListener(EventNames.GameStart, OnGameStart);
            EventManager.instance.AddListener(EventNames.StageEnd, OnStageEnd);
            EventManager.instance.AddListener(EventNames.StageSuccess, OnStageSuccess);
            EventManager.instance.AddListener(EventNames.StageFail, OnStageFail);
            EventManager.instance.AddListener(EventNames.LevelSuccess, OnLevelSuccess);
            EventManager.instance.AddListener(EventNames.LevelAgain, OnLevelAgain);
            EventManager.instance.AddListener(EventNames.StartMovement, () => isPlaying = true);
            EventManager.instance.AddListener(EventNames.StopMovement, () => isPlaying = false);
        }
        
        private void SetLevel()
        {
            stagePoolManager.Init();
            collectablePoolManager.Init();

            currentLevelNumber = PersistenceManager.GetCurrentLevelNumber();
            currentLevelId = PersistenceManager.GetCurrentLevelId();
            currentStageIndex = 0;
            
            levelManager.GenerateStartLevels(currentLevelNumber);
            currentLevel = levelManager.GetLevelByNumber(currentLevelNumber);
            currentStage = currentLevel.GetStageByIndex(currentStageIndex);
            currentStage.ActivateCollectables();
        }
        
        private void OnGameStart()
        {
            EventManager.instance.TriggerEvent(EventNames.StartMovement);
        }

        private void OnStageEnd()
        {
            EventManager.instance.TriggerEvent(EventNames.StopMovement);

            var stageEndSeq = DOTween.Sequence();
            
            if (currentStage.IsBasketFull)
            {
                if (currentStageIndex == currentLevel.GetStageCount() - 1)
                {
                    stageEndSeq.AppendCallback(() => EventManager.instance.TriggerEvent(EventNames.LevelSuccess));
                    return;
                }
                
                stageEndSeq.AppendCallback(() => EventManager.instance.TriggerEvent(EventNames.StageSuccess));
                stageEndSeq.AppendInterval(1f);
                stageEndSeq.AppendCallback(() =>
                {
                    EventManager.instance.TriggerEvent(EventNames.StartMovement);
                });
                
            }
            else
            {
                EventManager.instance.TriggerEvent(EventNames.StageFail);
            }
        }

        private void OnStageSuccess()
        {
            currentStage.OnSuccess();
            levelManager.AddCompletedStage(currentStage);
            currentStageIndex++;
            currentStage = currentLevel.GetStageByIndex(currentStageIndex);
            currentStage.ActivateCollectables();

            EventManager.instance.TriggerEvent(EventNames.ShowScreenRequested, typeof(StageSuccessScreen), null);
        }
        
        private void OnStageFail()
        {
            currentStage.OnFail();
            EventManager.instance.TriggerEvent(EventNames.ShowScreenRequested, typeof(StageFailScreen), null);
        }
        
        private void OnLevelAgain()
        {
            currentStage.Init(currentLevel.GetLevelData().stages[currentStageIndex]);
            currentStage.ActivateCollectables();
            character.transform.position = new Vector3(0, character.transform.position.y, currentStage.transform.position.z);
            EventManager.instance.TriggerEvent(EventNames.StartMovement);
        }
        
        private void OnLevelSuccess()
        {
            currentStage.OnSuccess();
            levelManager.AddCompletedStage(currentStage);
            currentStageIndex = 0;
            currentLevelNumber++;
            currentLevel = levelManager.GetLevelByNumber(currentLevelNumber);
            currentLevelId = currentLevel.GetLevelId();
            currentStage = currentLevel.GetStageByIndex(currentStageIndex);
            currentStage.ActivateCollectables();
            
            PersistenceManager.SetCurrentLevelNumber(currentLevelNumber);
            PersistenceManager.SetCurrentLevelId(currentLevelId);
            EventManager.instance.TriggerEvent(EventNames.ShowScreenRequested, typeof(LevelSuccessScreen), null);
        }
        
    }
    
}

