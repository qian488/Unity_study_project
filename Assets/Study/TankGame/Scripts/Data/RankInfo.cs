using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.Data
{
    public class RankInfo
    {
        public string playerName;
        public int score;
        public float time;

        public RankInfo() { }

        public RankInfo(string playerName, int score, float time)
        {
            this.playerName = playerName;
            this.score = score;
            this.time = time;
        }
    }

    public class RankList
    {
        public List<RankInfo> rankList;
    }
}
