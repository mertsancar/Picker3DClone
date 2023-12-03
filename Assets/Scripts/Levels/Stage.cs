using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Collectables;
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
        public bool IsBasketFull => _basketCounter >= basketCapacity;
        public int basketCapacity;
        private int _basketCounter;
        
        public void Init(StageData stageData)
        {
            basketCapacity = stageData.basketCapacity;
            collectables.Init(stageData.collectables);
        }
        
        private void Start()
        {
            stageBase.basketCounterText.text = _basketCounter + "/" + basketCapacity;
            EventManager.instance.AddListener(EventNames.UpdateBasketCounter, UpdateBasketCounter);
        }
        
        public void OnSuccess()
        {
            stageBase.OnSuccess();
        }
        
        private void UpdateBasketCounter()
        {
            _basketCounter++;
            stageBase.basketCounterText.text = _basketCounter + "/" + basketCapacity;
        }
        
    }
    
}

