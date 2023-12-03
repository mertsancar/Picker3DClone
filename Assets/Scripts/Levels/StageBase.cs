using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Levels
{
    public class StageBase : MonoBehaviour
    {
        public Transform doors;
        public Transform missingWay;
        public TMP_Text basketCounterText;
        
        public void OnSuccess()
        {
            var seq = DOTween.Sequence();

            seq.AppendCallback(GetMissingWay);
            seq.AppendInterval(.75f);
            seq.AppendCallback(OpenDoors);
            seq.AppendInterval(.75f);
            
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
        
    }
    
}

