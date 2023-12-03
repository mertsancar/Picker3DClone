using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Levels
{
    public class StagePoolManager : MonoBehaviour
    {
        private List<Stage> _stagePool;
        public int poolSize => _stagePool.Count;
        public int poolCapacity;
        public GameObject stageObjectPrefab;

        public void Init()
        {
            _stagePool = new List<Stage>();

            for (int i = 0; i < poolCapacity; i++)
            {
                var go = Instantiate(stageObjectPrefab, transform).GetComponent<Stage>();
                PushToPool(go);
            }
        }

        public void PushToPool(Stage stage)
        {
            _stagePool.Add(stage);
            stage.transform.SetParent(transform);
            stage.gameObject.SetActive(false);
        }

        public Stage PopFromPool()
        {
            var stage = _stagePool.Last();
            _stagePool.Remove(stage);
            stage.gameObject.SetActive(true);
            return stage;
        }
        
        
    }
    
}

