using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

namespace Levels
{
    public class StagePoolManager : MonoBehaviour
    {
        [SerializeField] private GameObject stageObjectPrefab;
        public int poolSize => _stagePool.Count;
        private List<Stage> _stagePool;

        public void Init()
        {
            _stagePool = new List<Stage>();

            for (int i = 0; i < GameController.Instance.levelManager.generateLevelCountOnStart * 3; i++)
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

