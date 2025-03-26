using System.Collections;
using System.Collections.Generic;
using TankGame.AGUI;
using TankGame.Data;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

namespace TankGame.GamePlay
{
    public class SettingPanel : BasePanel<SettingPanel>
    {
        public CustomGUIButton backBtn;
        public CustomGUISlider musicSlider;
        public CustomGUISlider soundSlider;

        void Start()
        {
            backBtn.clickEvent += () =>
            {
                if(SceneManager.GetActiveScene().name == "TankStart") StartPanel.Instance.Show();
                Hide();
            };

            musicSlider.changeValue += (value) => GameDataManager.Instance.ChangeMusicVolume(value);

            soundSlider.changeValue += (value) => GameDataManager.Instance.ChangeSoundVolume(value);

            Hide();
        }

        public void UpdatePanelInfo()
        {
            MusicData musicData = GameDataManager.Instance.musicData;
            musicSlider.nowValue = musicData.musicVolume;
            soundSlider.nowValue = musicData.soundVolume;
        }

        public override void Show()
        {
            base.Show();
            UpdatePanelInfo();
        }

        public override void Hide()
        {
            base.Hide();
            Time.timeScale = 1;
        }
    }
}
