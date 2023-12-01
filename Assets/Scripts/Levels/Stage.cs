using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Levels
{
    public class Stage : MonoBehaviour
    {
        public Transform collectables;
        public Transform missingWay;
        public Transform door;
        public TMP_Text poolItemCountText;
        public bool IsPoolFull => poolCounter >= poolCapacity;
        
        [SerializeField] private int poolCapacity;
        private int poolCounter;
        

        public void Success()
        {
            var seq = DOTween.Sequence();

            seq.AppendCallback(GetMissingWay);
            seq.AppendInterval(.75f);
            seq.AppendCallback(OpenDoors);
            seq.AppendInterval(.75f);
            
        }

        private void UpdatePoolCounter()
        {
            poolCounter++;
            poolItemCountText.text = poolCounter + "/" + poolCapacity;
        } 
            
        private void GetMissingWay()
        {
            missingWay.gameObject.SetActive(true);
            missingWay.DOMove(new Vector3(missingWay.position.x, 0, missingWay.position.z), .75f).SetEase(Ease.OutBounce);
        }
        
        private void OpenDoors()
        {
            var leftDoor = door.GetChild(0);
            var rightDoor = door.GetChild(1);

            leftDoor.transform.DORotate(new Vector3(leftDoor.rotation.x, leftDoor.rotation.y, 75f), .75f);
            rightDoor.transform.DORotate(new Vector3(leftDoor.rotation.x, leftDoor.rotation.y, -75f), .75f);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                UpdatePoolCounter();
            }
        }
    }
    
}

