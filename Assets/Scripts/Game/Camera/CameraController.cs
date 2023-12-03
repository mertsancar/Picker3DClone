using Managers;
using UnityEngine;

namespace Game.Camera
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public float speed = 5;
        public Vector3 offset;

        void FixedUpdate()
        {
            if (target || GameController.instance.isPlaying)
            {
                Vector3 newPosition = target.position + offset;
                transform.position = Vector3.Slerp(transform.position, newPosition, Time.deltaTime * speed);
            }
        }
    }
    
}

