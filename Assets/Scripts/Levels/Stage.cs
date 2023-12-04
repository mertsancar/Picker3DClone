using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Collectables;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Levels
{
    public class Stage : MonoBehaviour
    {
        public const float stageLength = 24.97f; 
        
        public StageBase stageBase;
        public Collectables collectables;
        public bool IsBasketFull => basketCounter >= basketCapacity;
        public int basketCapacity;
        public int basketCounter;
        
        public void Init(StageData stageData)
        {
            stageBase.ResetStageBase();
            
            basketCounter = 0;
            basketCapacity = stageData.basketCapacity;
            stageBase.basketCounterText.text = basketCounter + "/" + basketCapacity;
            
            collectables.Init(stageData.collectables);
        }
        
        private void Start()
        {
            basketCounter = 0;
            stageBase.basketCounterText.text = basketCounter + "/" + basketCapacity;
        }
        
        public void OnSuccess()
        {
            foreach (var collectable in collectables.CollectableList)
            {
                collectable.transform.DOScale(0, .25f).OnComplete(() =>
                    GameController.instance.collectablePoolManager.PushToPool(collectable));
            }
            stageBase.OnSuccess();
        }

        public void InitForEditor(StageData stageData)
        {
            stageBase.ResetStageBase();
            
            basketCounter = 0;
            basketCapacity = stageData.basketCapacity;
            collectables.InitForEditor(stageData.collectables);
        }
    }
    
}

