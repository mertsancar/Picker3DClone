using System;
using System.Collections.Generic;
using Game.Collectables;
using UnityEngine;

namespace Levels
{
    [Serializable]
    public struct LevelData
    {
        public int levelId;
        public List<StageData> stages;
    }

    [Serializable]
    public struct StageData
    {
        public int basketCapacity;
        public List<CollectableItemData> collectables;
    }
    
    [Serializable]
    public struct CollectableItemData
    {
        public CollectableType type;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }
}

