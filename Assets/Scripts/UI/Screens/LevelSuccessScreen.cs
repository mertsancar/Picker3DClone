using Managers;
using UnityEngine.UI;

namespace UI
{
    public class LevelSuccessScreen : BaseScreen
    {
        public override void Prepare(object param)
        {
            base.Prepare(param);
        }

        public void OnClickNextButton()
        {
            HideScreen();
            
            EventManager.instance.TriggerEvent(EventNames.StartMovement);
            GameController.instance.isPlaying = true;
        }
    }
    
}

