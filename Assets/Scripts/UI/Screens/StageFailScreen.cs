using Managers;

namespace UI.Screens
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

