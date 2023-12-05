using UnityEngine;

namespace Game.Collectables
{
    public abstract class BaseCollectable : MonoBehaviour
    {
        public CollectableType type;
        
        
    }

    public enum CollectableType
    {
        Cube,
        Sphere,
        Capsule
    }
    
}

