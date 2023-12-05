﻿using System;
using System.Collections.Generic;
using Game.Collectables;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Levels
{
    public class StageEditor : MonoBehaviour
    {
        private Collectables _collectables; 
        private List<BaseCollectable> _collectableList = new List<BaseCollectable>(); 
        public int basketCapacity;
        
        public void InitForEditor(StageData stageData)
        {
            basketCapacity = stageData.basketCapacity;
            _collectables = transform.GetChild(0).GetComponent<Collectables>();
            CollectablesInitForEditor(stageData.collectables);
        }
        
        private void CollectablesInitForEditor(List<CollectableItemData> collectableItemsData)
        {
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
                var go = Instantiate(collectablePrefab, _collectables.transform);
                go.transform.localPosition = collectableItemData.position;
                go.transform.localRotation = collectableItemData.rotation;
                go.transform.localScale = collectableItemData.scale;
            }
        }
        
        public List<BaseCollectable> GetCollectables()
        {
            if (_collectables == null) _collectables = transform.GetChild(0).GetComponent<Collectables>();

            _collectableList = new List<BaseCollectable>();
            for (int i = 0; i < _collectables.transform.childCount; i++)
            {
                _collectableList.Add(_collectables.transform.GetChild(i).GetComponent<BaseCollectable>());
            }
            return _collectableList;
        }
        
        public void AddCubeCollectable()
        {
            Debug.Log("Added Collectable!");
            
            var prefabPath = "Assets/Prefabs/Collectables/Cube.prefab";
            AddCollectable(prefabPath);
        }
        
        public void AddSphereCollectable()
        {
            Debug.Log("Added Collectable!");
            
            var prefabPath = "Assets/Prefabs/Collectables/Sphere.prefab";
            AddCollectable(prefabPath);
        }
        
        public void AddCapsuleCollectable()
        {
            Debug.Log("Added Collectable!");
            
            var prefabPath = "Assets/Prefabs/Collectables/Capsule.prefab";
            AddCollectable(prefabPath);
        }

        private void AddCollectable(string prefabPath)
        {
            var collectablePrefab = PrefabUtility.LoadPrefabContents(prefabPath);
            var collectableObject = Instantiate(collectablePrefab, transform.GetChild(0));

            Selection.objects = new Object[] { collectableObject };
        }
        
        public void DeleteLastCollectable()
        {
            Debug.Log("Last Collectable deleted!");
            var collectables = transform.GetChild(0);
            DestroyImmediate(collectables.GetChild(collectables.childCount - 1).gameObject);
        }
        
    }
    
}

