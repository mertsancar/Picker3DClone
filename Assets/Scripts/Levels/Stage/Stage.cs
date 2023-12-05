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
        
        public Transform doors;
        public Transform missingWay;
        public BoxCollider stageEndPoint;
        public TMP_Text basketCounterText;
        public Collectables collectables;
        public int basketCapacity;
        private int _basketCounter;
        public bool IsBasketFull => _basketCounter >= basketCapacity;
        
        public void Init(StageData stageData)
        {
            ResetStage();
            
            _basketCounter = 0;
            basketCapacity = stageData.basketCapacity;
            basketCounterText.text = _basketCounter + "/" + basketCapacity;
            
            collectables.Init(stageData.collectables);

        }
        
        private void ResetStage()
        {
            missingWay.position = new Vector3(missingWay.transform.position.x, -1, missingWay.transform.position.z);
            missingWay.gameObject.SetActive(false);
            
            var leftDoor = doors.GetChild(0);
            var rightDoor = doors.GetChild(1);

            leftDoor.transform.rotation = new Quaternion(0, 0, 0, 0);
            rightDoor.transform.rotation = new Quaternion(0, 0, 0, 0);

            stageEndPoint.enabled = true;
        }

        public void UpdateBasketCounter()
        {
            _basketCounter++;
            basketCounterText.text = _basketCounter + "/" + basketCapacity;
        }
        
        public void OnSuccess()
        {
            RemoveCollectables();
            
            var seq = DOTween.Sequence();
            seq.AppendCallback(GetMissingWay);
            seq.AppendInterval(.75f);
            seq.AppendCallback(OpenDoors);
            seq.AppendInterval(.75f);
        }
        
        public void OnFail()
        {
            RemoveCollectables();
        }

        private void RemoveCollectables()
        {
            foreach (var collectable in collectables.CollectableList)
            {
                collectable.transform.DOScale(0, .25f).OnComplete(() =>
                    GameController.instance.collectablePoolManager.PushToPool(collectable));
            }
        }
        
        private void GetMissingWay()
        {
            missingWay.gameObject.SetActive(true);
            missingWay.DOMove(new Vector3(missingWay.position.x, 0, missingWay.position.z), .75f).SetEase(Ease.OutBounce);
        }
            
        private void OpenDoors()
        {
            var leftDoor = doors.GetChild(0);
            var rightDoor = doors.GetChild(1);

            leftDoor.transform.DORotate(new Vector3(leftDoor.rotation.x, leftDoor.rotation.y, 75f), .75f);
            rightDoor.transform.DORotate(new Vector3(leftDoor.rotation.x, leftDoor.rotation.y, -75f), .75f);
        }

#if UNITY_EDITOR
        public void InitForEditor(StageData stageData)
        {
            ResetStage();
            
            _basketCounter = 0;
            basketCapacity = stageData.basketCapacity;
            collectables.InitForEditor(stageData.collectables);
        }
#endif
    }
    
}

