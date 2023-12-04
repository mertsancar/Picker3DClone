using System;
using System.Collections.Generic;
using System.Linq;
using Levels;
using UnityEngine;

namespace Game.Collectables
{
    public class CollectablePoolManager : MonoBehaviour
    {
        private Dictionary<CollectableType, List<BaseCollectable>> _collectablesPool;
        private int poolCapacity = 15;
        public GameObject cubeObjectPrefab;
        public GameObject sphereObjectPrefab;
        public GameObject capsuleObjectPrefab;


        public void Init()
        {
            _collectablesPool = new Dictionary<CollectableType, List<BaseCollectable>>();

            var collectableTypeList = Enum.GetValues(typeof(CollectableType)).Cast<CollectableType>().ToList();
            
            foreach (var collectableType in collectableTypeList)
            {
                _collectablesPool[collectableType] = new List<BaseCollectable>();
                
                for (int j = 0; j < poolCapacity; j++)
                {
                    var collectable = CollectableFactory(collectableType);
                    PushToPool(collectable);
                }
            }
            
        }
        
        public void PushToPool(BaseCollectable collectable)
        {
            var pool = _collectablesPool[collectable.type];
            pool.Add(collectable);
            collectable.transform.SetParent(transform);
            collectable.gameObject.SetActive(false);
        }

        public BaseCollectable PopFromPool(CollectableType collectableType)
        {
            var pool = _collectablesPool[collectableType];
            if (pool.Count == 0) PushToPool(CollectableFactory(collectableType));
            var collectable = pool[0];
            pool.Remove(collectable);
            collectable.gameObject.SetActive(true);
            return collectable;
        }
        
        private BaseCollectable CollectableFactory(CollectableType type)
        {
            switch (type)
            {
                case CollectableType.Cube:
                    return Instantiate(cubeObjectPrefab, transform).GetComponent<CubeCollectable>();
                case CollectableType.Sphere:
                    return Instantiate(sphereObjectPrefab, transform).GetComponent<SphereCollectable>();
                case CollectableType.Capsule:
                    return Instantiate(capsuleObjectPrefab, transform).GetComponent<CapsuleCollectable>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
    
}

