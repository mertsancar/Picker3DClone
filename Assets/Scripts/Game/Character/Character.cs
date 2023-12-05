using DG.Tweening;
using Managers;
using UnityEngine;

namespace Game.Character
{
    public class Character : MonoBehaviour
    {
        [HideInInspector] public Vector3 direction; //TODO-mertsancar
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Picker picker;
        
        [SerializeField] private float speed;
        [SerializeField] private float dragSpeed;

        private bool _isMoving;
        
        private void Awake()
        {
            EventManager.instance.AddListener(EventNames.StartMovement, StartMovement);
        }
        
        private void FixedUpdate()
        {
            if (!_isMoving)
            {
                rb.velocity = Vector3.zero;
                return;
            }

            ForwardMovement();
        }
        
        private void ForwardMovement()
        {
            rb.velocity = Vector3.ClampMagnitude((direction * dragSpeed), 7) + (Vector3.forward * speed);
        }
        
        private void StartMovement()
        {
            _isMoving = true;
        }
        
        private void StopMovement()
        {
            _isMoving = false;
        }
        
        private void OnStageEndPoint()
        {
            var seq = DOTween.Sequence();

            seq.AppendCallback(StopMovement);
            seq.AppendInterval(.1f);
            seq.AppendCallback(picker.PushCollectedItems);
            seq.AppendInterval(2f);
            seq.OnComplete(() => EventManager.instance.TriggerEvent(EventNames.StageEnd));
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("StageEndPoint"))
            {
                OnStageEndPoint();
                other.enabled = false;
            }
        }
    }
    
    
}

