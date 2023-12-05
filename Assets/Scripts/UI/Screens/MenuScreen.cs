using Managers;
using UnityEngine;

namespace UI.Screens
{
    public class MenuScreen : BaseScreen
    {
        public void OnDragToStart()
        {
            EventManager.instance.TriggerEvent(EventNames.GameStart);
            HideScreen();
        }
        
    }
    
}

