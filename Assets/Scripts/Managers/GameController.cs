using System;
using DG.Tweening;
using Game.Character;
using Levels;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;

        public Character character;
        public Level currentLevel;
        public Stage currentStage;

        public int currentLevelIndex;
        public int currentStageIndex;
        
        private void Awake()
        {
            instance = this;
            Application.targetFrameRate = 600;
            
            SetGameEvents();
        }

        private void SetGameEvents()
        {
            EventManager.instance.AddListener(EventNames.GameStart, GameStart);
            EventManager.instance.AddListener(EventNames.StageEnd, StageEnd);
            EventManager.instance.AddListener(EventNames.StageSuccess, StageSuccess);
            EventManager.instance.AddListener(EventNames.StageFail, StageFail);
            EventManager.instance.AddListener(EventNames.LevelSuccess, LevelSuccess);
        }
        
        private void Start()
        {
            EventManager.instance.TriggerEvent(EventNames.GameStart);
        }
        
        private void GameStart()
        {
            currentStageIndex = 0;
        }
        
        private void StageEnd()
        {
            currentStage = currentLevel.stages.GetChild(currentStageIndex).GetComponent<Stage>();
            
            if (currentStage.IsPoolFull)
            {
                StageSuccess();
            }
            else
            {
                StageFail();
            }
        }

        private void StageSuccess()
        {
            var seq = DOTween.Sequence();

            seq.AppendCallback(currentStage.Success);
            seq.AppendCallback(() =>
            {
                currentStageIndex++;
                if (currentStageIndex > currentLevel.stages.childCount - 1) currentStageIndex = 0;
            });
            seq.AppendCallback(() => EventManager.instance.TriggerEvent(EventNames.ShowScreenRequested, typeof(StageSuccessScreen), null));
            seq.AppendInterval(1f);
            seq.AppendCallback(() => EventManager.instance.TriggerEvent(EventNames.StartMovement));
        }
        
        private void StageFail()
        {
            EventManager.instance.TriggerEvent(EventNames.ShowScreenRequested, typeof(StageFailScreen), null);
        }
        
        private void LevelSuccess()
        {
            EventManager.instance.TriggerEvent(EventNames.ShowScreenRequested, typeof(LevelSuccessScreen), null);
        }
        
    }
    
}

