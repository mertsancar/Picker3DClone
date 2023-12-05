using Managers;
using UnityEngine;

namespace Game.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed = 5;
        [SerializeField] private Vector3 offset;

        private void FixedUpdate()
        {
            if (target || GameController.Instance.isPlaying)
            {
                Vector3 newPosition = target.position + offset;
                transform.position = Vector3.Slerp(transform.position, newPosition, Time.deltaTime * speed);
            }
        }
    }
    
}

