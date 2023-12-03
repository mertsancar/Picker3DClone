using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEditor;
using UnityEngine;

public class MyTest : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(InstantiateTest());
    }

    private IEnumerator InstantiateTest()
    {
        var levelPrefabPath = "Assets/Resources/Levels/Prefabs/level" + 10 + ".prefab";
        var templevel = PrefabUtility.LoadPrefabContents(levelPrefabPath).GetComponent<Level>();
        var totalStageCount = templevel.stages.childCount;
        
        
        for (int i = 0; i < totalStageCount; i++)
        {
            Instantiate(templevel.stages.GetChild(i), transform);
            yield return new WaitForSeconds(1f);
        }
        

    }
}
