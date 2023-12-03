using System;
using System.Collections.Generic;
using Levels;
using Managers;
using UnityEditor;
using UnityEngine;

namespace Game.Collectables
{
    public class Collectables : MonoBehaviour
    {
        private List<BaseCollectable> collectableList;
        public List<BaseCollectable> CollectableList
        {
            get
            {
                AddCollectablesToList();
                return collectableList;
            }
            set
            {
                collectableList = value;
            }
        }

        public void Init(List<CollectableItemData> collectableItemsData)
        {
            ResetCollectables();
            
            foreach (var collectableItemData in collectableItemsData)
            {
                var go = GameController.instance.collectablePoolManager.PopFromPool(collectableItemData.type); //TODO-mertsancar: object pooling WIP
                go.transform.localPosition = collectableItemData.position;
                go.transform.localRotation = collectableItemData.rotation;
                go.transform.localScale = collectableItemData.scale;
            }
        }

        private void ResetCollectables()
        {
            if (transform.childCount != 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
            
            collectableList = new List<BaseCollectable>();
        }

        private void AddCollectablesToList()
        {
            collectableList = new List<BaseCollectable>();
            for (int i = 0; i < transform.childCount; i++)
            {
                collectableList.Add(transform.GetChild(i).GetComponent<BaseCollectable>());
            }
        }
        
        public void InitForEditor(List<CollectableItemData> collectableItemsData)
        {
            ResetCollectables();
            
            foreach (var collectableItemData in collectableItemsData)
            {
                var collectablePrefabPath = "";
                switch (collectableItemData.type)
                {
                    case CollectableType.Cube:
                        collectablePrefabPath = "Assets/Prefabs/Collectables/Cube.prefab";
                        break;
                    case CollectableType.Sphere:
                        collectablePrefabPath = "Assets/Prefabs/Collectables/Sphere.prefab";
                        break;
                    case CollectableType.Capsule:
                        collectablePrefabPath = "Assets/Prefabs/Collectables/Capsule.prefab";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var collectablePrefab = PrefabUtility.LoadPrefabContents(collectablePrefabPath);
                var go = Instantiate(collectablePrefab, transform);
                go.transform.localPosition = collectableItemData.position;
                go.transform.localRotation = collectableItemData.rotation;
                go.transform.localScale = collectableItemData.scale;
            }
        }
        
    }
    
}

