using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Levels
{
    public class Stage : MonoBehaviour
    {
        public Transform collectables;
        public Transform way;
        public Transform missingWay;
        public Transform door;
        
        public Transform pool;
        public TMP_Text poolCountText;
        public int poolCapacity;
        private int poolCounter;


        public void StageSuccess()
        {
            var seq = DOTween.Sequence();

            seq.AppendCallback(GetMissingWay);
            seq.AppendInterval(1f);
            seq.AppendCallback(OpenDoors);
            
        }
        
        private void GetMissingWay()
        {
            missingWay.DOMove(new Vector3(missingWay.position.x, 0, missingWay.position.z), 1f);
        }
        
        private void OpenDoors()
        {
            var leftDoor = door.GetChild(0);
            var rightDoor = door.GetChild(1);

            leftDoor.transform.DORotate(new Vector3(leftDoor.rotation.x, leftDoor.rotation.y, 90f), 1f);
            rightDoor.transform.DORotate(new Vector3(leftDoor.rotation.x, leftDoor.rotation.y, -90f), 1f);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                poolCounter++;
                poolCountText.text = poolCounter + "/" + poolCapacity;
            }
        }
    }
    
}

