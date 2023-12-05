using Managers;
using UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class StageFailScreen : BaseScreen
    {
        public void OnClickAgainButton()
        {
            EventManager.instance.TriggerEvent(EventNames.LevelAgain);
            HideScreen();
        }
    }
    
}

