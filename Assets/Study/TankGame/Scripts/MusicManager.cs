using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankGame.Data;

namespace TankGame.GamePlay
{
    public class MusicManager : MonoBehaviour
    {
        private static MusicManager instance;
        public static MusicManager Instance { get => instance; }

        private AudioSource audioSource;
        private void Awake()
        {
            instance = this;
            audioSource = this.GetComponent<AudioSource>();
            ChangeValue(GameDataManager.Instance.musicData.musicVolume);
        }

        public void ChangeValue(float value)
        {
            audioSource.volume = value;
        }
    }
}

