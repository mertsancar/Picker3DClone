using DG.Tweening;
using Game.Collectables;
using TMPro;
using UnityEngine;

namespace Levels
{
    public class Stage : MonoBehaviour
    {
        public const float stageLength = 29.8f; 
        
        [SerializeField] private Transform doors;
        [SerializeField] private Transform missingWay;
        [SerializeField] private BoxCollider stageEndPoint;
        [SerializeField] private TMP_Text basketCounterText;
        [SerializeField] private Collectables collectables;
        private int _basketCapacity;
        private int _basketCounter;
        public bool IsBasketFull => _basketCounter >= _basketCapacity;
        
        public void Init(StageData stageData)
        {
            ResetStage();
            
            _basketCounter = 0;
            _basketCapacity = stageData.basketCapacity;
            basketCounterText.text = _basketCounter + "/" + _basketCapacity;
            
            collectables.Init(stageData.collectables);

        }
        
        public void UpdateBasketCounter()
        {
            _basketCounter++;
            basketCounterText.text = _basketCounter + "/" + _basketCapacity;
        }

        public void ActivateCollectables()
        {
            collectables.ActivateCollectables();
        }
        
        public void OnSuccess()
        {
            collectables.RemoveCollectables();
            
            var seq = DOTween.Sequence();
            seq.AppendCallback(GetMissingWay);
            seq.AppendInterval(.75f);
            seq.AppendCallback(OpenDoors);
            seq.AppendInterval(.75f);
        }
        
        public void OnFail()
        {
            collectables.RemoveCollectables();
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
        

        
    }
    
}

