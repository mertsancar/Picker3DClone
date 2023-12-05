using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("Game");
        }
    }
    
}

