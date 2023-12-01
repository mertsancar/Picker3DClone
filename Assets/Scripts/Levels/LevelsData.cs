using System;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelsData", order = 1)]
    public class LevelsData : ScriptableObject
    {
        public List<LevelData> levelsData;
    }

    [Serializable]
    public struct LevelData
    {
        public int levelId;
        public List<StageData> stages;
    }

    [Serializable]
    public struct StageData
    {
        public Transform way;
        public List<Transform> collectables;
        public Transform pool;
        public Transform door;
    }
}

