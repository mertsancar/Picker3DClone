using Managers;
using UnityEngine;

namespace UI
{
    public class StageFailScreen : BaseScreen
    {
        public override void Prepare(object param)
        {
            base.Prepare(param);
        }

        public void OnClickAgainButton()
        {
            HideScreen();
            GameController.instance.isPlaying = true;
        }
    }
    
}

