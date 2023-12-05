using Managers;
using UI.Screens;
using UnityEngine.UI;

namespace UI
{
    public class LevelSuccessScreen : BaseScreen
    {
        public void OnClickNextButton()
        {
            HideScreen();
            
            EventManager.instance.TriggerEvent(EventNames.StartMovement);
        }
    }
    
}

