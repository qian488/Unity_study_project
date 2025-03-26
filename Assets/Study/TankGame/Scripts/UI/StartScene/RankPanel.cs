using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankGame.AGUI;
using TankGame.Data;

namespace TankGame.GamePlay
{
    public class RankPanel : BasePanel<RankPanel>
    {
        public CustomGUIButton backBtn;

        private List<CustomGUILabel> labelRank = new List<CustomGUILabel>();
        private List<CustomGUILabel> labelScore = new List<CustomGUILabel>();
        private List<CustomGUILabel> labelName = new List<CustomGUILabel>();
        private List<CustomGUILabel> labelTime = new List<CustomGUILabel>();

        void Start()
        {
            for(int i = 1; i <= 6; i++)
            {
                labelRank.Add(transform.Find("LabelRank" + i).GetComponent<CustomGUILabel>());
                labelScore.Add(transform.Find("LabelScore" + i).GetComponent<CustomGUILabel>());
                labelName.Add(transform.Find("LabelName" + i).GetComponent<CustomGUILabel>());
                labelTime.Add(transform.Find("LabelTime" + i).GetComponent<CustomGUILabel>());
            }

            backBtn.clickEvent += () =>
            {
                StartPanel.Instance.Show();
                Hide();
            };

            // GameDataManager.Instance.AddRankData(new RankInfo("AAA", 100, 100));

            Hide();
        }

        public override void Show()
        {
            base.Show();
            UpdatePanelInfo();
        }

        public void UpdatePanelInfo()
        {
            List<RankInfo> rankList = GameDataManager.Instance.rankData.rankList;
            for(int i = 0; i < rankList.Count; i++)
            {
                labelScore[i].content.text = rankList[i].score.ToString();
                labelName[i].content.text = rankList[i].playerName;
                int[] hms = GetTime2hms(rankList[i].time);
                labelTime[i].content.text = hms[0] + "h" + hms[1] + "m" + hms[2] + "s";
            }
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

    }
}
