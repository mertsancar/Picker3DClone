using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private TMP_Text currentLevelNumberText;
    [SerializeField] private TMP_Text nextLevelNumberText;
    [SerializeField] private List<Slider> stageSliders;
    private int completedStageCount;
    
    void Start()
    {
        UpdateLevelTexts();
        
        completedStageCount = 0;
        
        EventManager.instance.AddListener(EventNames.StageSuccess, OnStageSuccess);
        EventManager.instance.AddListener(EventNames.LevelSuccess, OnLevelSuccess);
    }
    
    private void UpdateLevelTexts()
    {
        var currentLevelIndex = PersistenceManager.GetCurrentLevelNumber();
        currentLevelNumberText.text = currentLevelIndex.ToString();
        nextLevelNumberText.text = (currentLevelIndex + 1).ToString();
    }

    private void OnStageSuccess()
    {
        completedStageCount++;
        stageSliders[completedStageCount-1].fillRect.gameObject.SetActive(true);
        stageSliders[completedStageCount-1].DOValue(100f, 1f);
    }

    private void OnLevelSuccess()
    {
        var levelSuccessSeq = DOTween.Sequence();

        levelSuccessSeq.AppendCallback(OnStageSuccess);
        levelSuccessSeq.AppendInterval(1f);
        levelSuccessSeq.AppendCallback(() =>
        {
            for (int i = 0; i < completedStageCount; i++)
            {
                stageSliders[i].fillRect.gameObject.SetActive(false);
                stageSliders[i].value = 0;
            }
            completedStageCount = 0;
        });
        levelSuccessSeq.OnComplete(UpdateLevelTexts);
    }
    
    
}
