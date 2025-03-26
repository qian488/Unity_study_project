using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TankGame.AGUI;
using UnityEngine;

namespace TankGame.GamePlay
{
    public class GamePanel : BasePanel<GamePanel>
    {
        public CustomGUILabel labelScore;
        public CustomGUILabel labelTime;
        public CustomGUIButton buttonSetting;
        public CustomGUIButton buttonQuit;
        public CustomGUITexture textureHP;

        public int HP = 600;

        [HideInInspector]
        public int nowScore = 0;
        [HideInInspector]
        public float nowTime = 0;

        private int time;
        private int[] hms = new int[3] { 0, 0, 0 };

        private void Start()
        {
            buttonSetting.clickEvent += () =>
            {
                
                SettingPanel.Instance.Show();
                Time.timeScale = 0;
            };

            buttonQuit.clickEvent += () =>
            {
                QuitPanel.Instance.Show();
                Time.timeScale = 0;
            };
        }

        private void Update()
        {
            nowTime += Time.deltaTime;
            time = (int)nowTime;
            hms = GetTime2hms(time);
            labelTime.content.text = hms[0] + "h" + hms[1] + "m" + hms[2] + "s";
        }

        public void AddScore(int score)
        {
            nowScore += score;
            labelScore.content.text = nowScore.ToString();
        }

        public void UpdateHP(int maxHP, int hp)
        {
            textureHP.guiPos.width = (float)hp / maxHP * HP;
        }

        private int[] GetTime2hms(float time)
        {
            int[] hms = new int[3];
            int t = (int)time;
            hms[0] = t / 3600;
            hms[1] = (t % 3600) / 60;
            hms[2] = t % 60;
            return hms;
        }

        public override void Show()
        {
            base.Show();
            Time.timeScale = 1;
        }
    }
}

