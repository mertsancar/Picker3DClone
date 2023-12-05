using System;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using Managers;
using UnityEditor;
using UnityEngine;

namespace Game.Collectables
{
    public class Collectables : MonoBehaviour
    {
        private List<BaseCollectable> collectableList = new List<BaseCollectable>();

        public void Init(List<CollectableItemData> collectableItemsData)
        {
            ResetCollectables();
            
            foreach (var collectableItemData in collectableItemsData)
            {
                var collectableObject = GameController.Instance.collectablePoolManager.PopFromPool(collectableItemData.type);
                collectableList.Add(collectableObject);

                var collectableTransform = collectableObject.transform;
                collectableTransform.SetParent(transform);
                collectableTransform.localPosition = collectableItemData.position;
                collectableTransform.localRotation = collectableItemData.rotation;
                collectableTransform.localScale = collectableItemData.scale;
            }
        }
        
        public void RemoveCollectables()
        {
            foreach (var collectable in collectableList)
            {
                collectable.transform.DOScale(0, .25f).OnComplete(() =>
                {
                    collectableList.Remove(collectable);
                    GameController.Instance.collectablePoolManager.PushToPool(collectable);
                });
            }
        }
        
        private void ResetCollectables()
        {
            if (transform.childCount != 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var collectable = transform.GetChild(i).GetComponent<BaseCollectable>();
                    GameController.Instance.collectablePoolManager.PushToPool(collectable);
                }
            }
            
            collectableList = new List<BaseCollectable>();
        }
        
    }
    
}

