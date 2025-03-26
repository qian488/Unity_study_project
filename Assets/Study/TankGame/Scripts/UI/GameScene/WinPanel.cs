using System.Collections;
using System.Collections.Generic;
using TankGame.AGUI;
using TankGame.Data;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankGame.GamePlay
{
    public class WinPanel : BasePanel<WinPanel>
    {
        public CustomGUIInput input;
        public CustomGUIButton button;

        void Start()
        {
            button.clickEvent += () =>
            {
                Time.timeScale = 1;

                GameDataManager.Instance.AddRankData(new RankInfo(
                    input.content.text,
                    GamePanel.Instance.nowScore,
                    GamePanel.Instance.nowTime));

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
