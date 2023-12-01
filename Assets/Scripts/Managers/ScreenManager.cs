using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    
    public GameObject[] screens;

    private void Awake()
    {
        instance = this;
        EventManager.instance.AddListener(EventNames.ShowScreenRequested, ShowScreenRequested);
        EventManager.instance.AddListener(EventNames.HideScreenRequested, HideScreenRequested);
    }

    public bool IsAnyScreenOpen()
    {
        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i].activeInHierarchy)
            {
                return true;
            }
        }

        return false;
    }

    private void HideScreenRequested(object eventParams, object param)
    {
        HideScreen((Type)eventParams, param);
    }

    private void HideScreen(Type t, object param, object analytics = null)
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
    
    public void ShowScreen(GameObject screen, object param)
    {
        screen.SetActive(true);
        var canvasGroup = screen.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 0.5f);
        screen.GetComponent<BaseScreen>().Prepare(param);
        EventManager.instance.TriggerEvent(EventNames.ScreenShown, GetType());
    }

    private T ShowScreen<T>(object param) where T : BaseScreen
    {
        var screen = FindObjectOfType<T>();
        ShowScreen(screen.gameObject, param);
        return screen;
    }

    public Component ShowScreen(Type t, object param)
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

    private void HideScreenComplete()
    {
        HideAllScreens();
    }

    public void HideAllScreens()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void HideScreen(GameObject screen)
    {
        var canvasGroup = screen.GetComponent<CanvasGroup>();
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 0.5f);
    }
}