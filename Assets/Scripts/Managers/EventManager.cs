using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager instance;
            
        private Dictionary<string, Action> _eventDictionary0;
        private Dictionary<string, Action<object>> _eventDictionary1;
        private Dictionary<string, Action<object, object>> _eventDictionary2;
        private Dictionary<string, Action<object, object, object>> _eventDictionary3;

        private void Awake()
        {
            instance = this;
            SceneManager.sceneUnloaded += ClearListeners;
            SceneManager.sceneUnloaded += ClearAllTween;
        }

        public void AddListener(string eventName, Action listener)
        {
            if (_eventDictionary0 == null) _eventDictionary0 = new Dictionary<string, Action>();
            if (_eventDictionary0.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent += listener;
                _eventDictionary0[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                _eventDictionary0.Add(eventName, thisEvent);
            }
        }

        private void ClearListeners(Scene scene)
        {
            _eventDictionary0?.Clear();
            _eventDictionary1?.Clear();
            _eventDictionary2?.Clear();
            _eventDictionary3?.Clear();
        }

        private void ClearAllTween(Scene scene)
        {
            if (scene.name == "Initial") return;
            DOTween.KillAll();
        }
            
        public void AddListener(string eventName, Action<object, object> listener)
        {
            if (_eventDictionary2 == null) _eventDictionary2 = new Dictionary<string, Action<object, object>>();
            if (_eventDictionary2.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent += listener;
                _eventDictionary2[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                _eventDictionary2.Add(eventName, thisEvent);
            }
        }

        public void TriggerEvent(string eventName)
        {
            if (_eventDictionary0 == null)
            {
                Debug.LogWarning("[EventManager] TriggerEvent:: Event couldn't be triggered because there are no listeners");
                return;
            }

            if (_eventDictionary0.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.Invoke();
            }
        }

        public void TriggerEvent(string eventName, object arg1)
        {
            if (_eventDictionary1 == null)
            {
                Debug.LogWarning("[EventManager] TriggerEvent:: Event couldn't be triggered because there are no listeners");
                return;
            }

            if (_eventDictionary1.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.Invoke(arg1);
            }
        }
            
        public void TriggerEvent(string eventName, object arg1, object arg2)
        {
            if (_eventDictionary2 == null)
            {
                Debug.LogWarning("[EventManager] TriggerEvent:: Event couldn't be triggered because there are no listeners");
                return;
            }

            if (_eventDictionary2.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.Invoke(arg1, arg2);
            }
        }
    }
    
}



public static class EventNames
{
    public static readonly string ShowScreenRequested = "ShowScreenRequested";
    public static readonly string HideScreenRequested = "HideScreenRequested";
    public static readonly string ScreenClosed = "ScreenClosed";
    public static readonly string ScreenShown = "ScreenShown";
    public static readonly string GameStart = "GameStart";
    public static readonly string StartMovement = "StartMovement";
    public static readonly string StopMovement = "StopMovement";
    public static readonly string StageEnd = "StageEnd";
    public static readonly string StageSuccess = "StageSuccess";
    public static readonly string StageFail = "StageFail";
    public static readonly string LevelSuccess = "LevelSuccess";
    public static readonly string LevelAgain = "LevelAgain";

}