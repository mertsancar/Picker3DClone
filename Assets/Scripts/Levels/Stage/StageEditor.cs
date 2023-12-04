using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Levels
{
    public class StageEditor : MonoBehaviour
    {
        public Stage stage;

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
            var collectableObject = Instantiate(collectablePrefab, stage.collectables.transform);

            Selection.objects = new Object[] { collectableObject };
        }
        
        public void DeleteLastCollectable()
        {
            Debug.Log("Last Collectable deleted!");

            DestroyImmediate(stage.collectables.transform.GetChild(stage.collectables.transform.childCount - 1).gameObject);
        }
        
        
    }
    
}

