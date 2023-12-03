using System;
using System.Collections.Generic;
using Levels;
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
            foreach (var collectableItemData in collectableItemsData)
            {
                var collectablePrefab = PrefabUtility.LoadPrefabContents("Assets/Prefabs/Collectables/Cube.prefab");
                var go = Instantiate(collectablePrefab, transform).AddComponent<CubeCollectable>();//TODO-mertsancar: Object pooling for collectables
                go.transform.localPosition = collectableItemData.position;
                go.transform.localRotation = collectableItemData.rotation;
                go.transform.localScale = collectableItemData.scale;
            }
        }

        private void AddCollectablesToList()
        {
            collectableList = new List<BaseCollectable>();
            for (int i = 0; i < transform.childCount; i++)
            {
                collectableList.Add(transform.GetChild(i).GetComponent<BaseCollectable>());
            }
        }
        
    }
    
}

