using System;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public GameObject[] screens;

    private void Awake()
    {
        EventManager.instance.AddListener(EventNames.ShowScreenRequested, ShowScreenRequested);
        EventManager.instance.AddListener(EventNames.HideScreenRequested, HideScreenRequested);
    }

    private void HideScreenRequested(object eventParams, object param)
    {
        HideScreen((Type)eventParams, param);
    }

    private void HideScreen(Type t, object param)
    {
        var screens = (BaseScreen[])Resources.FindObjectsOfTypeAll(t);
        foreach (var baseScreenController in screens)
        {
            baseScreenController.HideScreen();
        }
    }

    private void ShowScreenRequested(object eventParams, object param)
    {
        ShowScreen((Type)eventParams, param);
    }
    
    private void ShowScreen(GameObject screen, object param)
    {
        screen.SetActive(true);
        var canvasGroup = screen.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 0.5f);
        screen.GetComponent<BaseScreen>().Prepare(param);
        EventManager.instance.TriggerEvent(EventNames.ScreenShown, GetType());
    }
    
    private Component ShowScreen(Type t, object param)
    {
        var screens = (BaseScreen[])Resources.FindObjectsOfTypeAll(t);
        foreach (var baseScreenController in screens)
        {
            var activeScene = SceneManager.GetActiveScene();
            if (baseScreenController.gameObject.scene == activeScene)
            {
                ShowScreen(baseScreenController.gameObject, param);
                return baseScreenController;
            }
        }

        return null;
    }

    public void HideScreenComplete()
    {
        HideAllScreens();
    }

    private void HideAllScreens()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void HideScreen(GameObject screen)
    {
        var canvasGroup = screen.GetComponent<CanvasGroup>();
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 0.5f);
    }
}