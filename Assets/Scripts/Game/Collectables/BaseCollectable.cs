using UnityEngine;

namespace Game.Collectables
{
    public abstract class BaseCollectable : MonoBehaviour
    {
        public CollectableType type;

        public void Init()
        {
            
        }
        
    }

    public enum CollectableType
    {
        Cube,
        Sphere,
        Capsule
    }
    
}

