using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 600;
        
        SetGameEvents();
    }

    private void SetGameEvents()
    {
        EventManager.instance.AddListener(EventNames.GameStart, GameStart);
        EventManager.instance.AddListener(EventNames.StageSuccess, LevelSuccess);
        EventManager.instance.AddListener(EventNames.LevelSuccess, LevelSuccess);
        EventManager.instance.AddListener(EventNames.LevelFail, LevelFail);
    }
    
    private void Start()
    {
        EventManager.instance.TriggerEvent(EventNames.GameStart);
    }
    
    private void GameStart()
    {
        throw new NotImplementedException();
    }

    private void StageSuccess()
    {
        
    }
    
    private void LevelSuccess()
    {
        
    }
    
    private void LevelFail()
    {
        
    }
}
