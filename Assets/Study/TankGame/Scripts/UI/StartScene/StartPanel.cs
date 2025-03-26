using System.Collections;
using System.Collections.Generic;
using TankGame.AGUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankGame.GamePlay
{
    public class StartPanel : BasePanel<StartPanel>
    {
        public CustomGUIButton startBtn;
        public CustomGUIButton settingBtn;
        public CustomGUIButton exitBtn;
        public CustomGUIButton rankBtn;

        void Start()
        {
            startBtn.clickEvent += () =>
            {
                LoadPanel.Instance.Show();
                StartCoroutine(LoadTankGameAsync());
                // Hide();
            };

            settingBtn.clickEvent += () =>
            {
                SettingPanel.Instance.Show();
                Hide();
            };

            exitBtn.clickEvent += () =>
            {
                Application.Quit();
            };

            rankBtn.clickEvent += () =>
            {
                RankPanel.Instance.Show();
                Hide();
            };
        }

        IEnumerator LoadTankGameAsync()
        {
            // 开始异步加载场景
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync("TankGame");

            asyncOp.allowSceneActivation = false;
            while (asyncOp.progress < 0.9f)
            {
                LoadPanel.Instance.bar.guiPos.width = asyncOp.progress * LoadPanel.Instance.bg.guiPos.width;
                yield return null;
            }

            yield return new WaitForSeconds(2.0f);
            asyncOp.allowSceneActivation = true;
        }

    }
}
