using System.Collections;
using System.Collections.Generic;
using TankGame.AGUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankGame.GamePlay
{
    public class QuitPanel : BasePanel<QuitPanel> {
        public CustomGUIButton buttonYes;
        public CustomGUIButton buttonNo;

        private void Start()
        {
            buttonYes.clickEvent += () =>
            {
                SceneManager.LoadScene("TankStart");
            };

            buttonNo.clickEvent += () =>
            {
                Hide();
            };

            Hide();
        }

        public override void Hide()
        {
            base.Hide();
            Time.timeScale = 1;
        }
    }

}
