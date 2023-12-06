using UnityEngine;

namespace Levels
{
    public class LevelParser
    {
        public static readonly string StagePrefabPath = "Assets/Prefabs/Levels/Stage/Stage.prefab";
        public static readonly string CubePrefabPath = "Assets/Prefabs/Collectables/Cube.prefab";
        public static readonly string SpherePrefabPath = "Assets/Prefabs/Collectables/Sphere.prefab";
        public static readonly string CapsulePrefabPath = "Assets/Prefabs/Collectables/Capsule.prefab";
        public static readonly string ConePrefabPath = "Assets/Prefabs/Collectables/Cone.prefab";
        
        public static LevelData GetLevelDataById(int levelId)
        {
            var dataPath = System.IO.File.ReadAllText(Application.dataPath + "/Resources/Levels/Level" + levelId + ".json");
            LevelData data = default;
            data.levelId = -1;
                
            try {
                data = JsonUtility.FromJson<LevelData>(dataPath);
            }
            catch {
                Debug.LogError("Error: The specified level " + levelId + " could not be found. The application has defaulted to the default level.");
            }

            return data;
        }
        
        public static void SetLevelDataById(LevelData levelData)
        {
            var data = JsonUtility.ToJson(levelData);
            System.IO.File.WriteAllText(Application.dataPath + "/Resources/Levels/Level" + levelData.levelId + ".json", data);
        }

    }
    
}

