using System.Collections;
using System.Collections.Generic;
using TankGame.AGUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankGame.GamePlay
{
    public class LosePanel : BasePanel<LosePanel>
    {
        public CustomGUIButton buttonOK;
        public CustomGUIButton buttonCancel;

        // Start is called before the first frame update
        void Start()
        {
            buttonOK.clickEvent += () =>
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("TankGame");
            };
            buttonCancel.clickEvent += () => 
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("TankStart");
            };
            Hide();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

