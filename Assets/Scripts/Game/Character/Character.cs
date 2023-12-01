using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Collectables;
using UnityEngine;

namespace Game.Character
{
    public class Character : MonoBehaviour
    {
        public Picker picker;

        private void Awake()
        {
            EventManager.instance.AddListener(EventNames.StartMovement, StartMovement);
        }

        private void StartMovement()
        {
            throw new System.NotImplementedException();
        }
        
        private void StopMovement()
        {
            throw new System.NotImplementedException();
        }
        
        private void OnStageEndPoint()
        {
            var seq = DOTween.Sequence();

            seq.AppendCallback(StopMovement);
            seq.AppendInterval(.25f);
            seq.AppendCallback(picker.PushCollectedItems);
            seq.AppendInterval(1.25f);
            
            EventManager.instance.TriggerEvent(EventNames.StageEnd);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("StageEndPoint"))
            {
                OnStageEndPoint();
            }
        }
    }
    
    
}

