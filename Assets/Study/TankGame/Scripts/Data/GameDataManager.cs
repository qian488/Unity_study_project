using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankGame.PrefsData;
using TankGame.GamePlay;

namespace TankGame.Data
{
    public class GameDataManager
    {
        private static GameDataManager instance = new GameDataManager();
        public static GameDataManager Instance { get => instance; }

        public MusicData musicData;
        public RankList rankData;

        private GameDataManager()
        {
            musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData;
            if(!musicData.notFirstTime)
            {
                musicData.isMusicOn = true;
                musicData.isSoundOn = true;
                musicData.musicVolume = 0.5f;
                musicData.soundVolume = 0.5f;
                musicData.notFirstTime = true;
                PlayerPrefsDataMgr.Instance.SaveData(musicData, "MusicData");
            }

            rankData = PlayerPrefsDataMgr.Instance.LoadData(typeof(RankList), "RankData") as RankList;
        }

        public void ChangeMusicVolume(float value)
        {
            musicData.musicVolume = value;
            GamePlay.MusicManager.Instance.ChangeValue(value);
            PlayerPrefsDataMgr.Instance.SaveData(musicData, "MusicData");
        }

        public void ChangeSoundVolume(float value)
        {
            musicData.soundVolume = value;
            PlayerPrefsDataMgr.Instance.SaveData(musicData, "MusicData");
        }

        public void AddRankData(RankInfo rankInfo)
        {
            rankData.rankList.Add(rankInfo);
            rankData.rankList.Sort((a, b) =>
            {
                if (a.score != b.score)
                {
                    return b.score - a.score;
                }
                else
                {
                    return (int)(a.time - b.time);
                }
            });
            if (rankData.rankList.Count > 6)
            {
                rankData.rankList.RemoveRange(6, rankData.rankList.Count - 6);
            }
            SaveRankData();
        }

        public void SaveRankData()
        {
            PlayerPrefsDataMgr.Instance.SaveData(rankData, "RankData");
        }

        public void ClearRankData()
        {
            rankData.rankList.Clear();
            SaveRankData();
        }
    }
}

