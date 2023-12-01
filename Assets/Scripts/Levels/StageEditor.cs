using UnityEditor;
using UnityEngine;

namespace Levels
{
    public class StageEditor : MonoBehaviour
    {
        public Stage stage;
        
        public int poolCapacity;

        public void AddCubeCollectable()
        {
            Debug.Log("Added Collectable!");
            
            var prefabPath = "Assets/Prefabs/Cube.prefab";
            AddCollectable(prefabPath);
        }
        
        public void AddSphereCollectable()
        {
            Debug.Log("Added Collectable!");
            
            var prefabPath = "Assets/Prefabs/Sphere.prefab";
            AddCollectable(prefabPath);
        }

        private void AddCollectable(string prefabPath)
        {
            var collectablePrefab = PrefabUtility.LoadPrefabContents(prefabPath);
            var collectableObject = Instantiate(collectablePrefab, stage.collectables);
        }
        
    }
    
}

