using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gal_first
{
    //音乐管理
    public class AudiosourceManger : MonoBehaviour
    {
        public static AudiosourceManger Instance { get; private set; }
        public AudioSource soundAudio;//特效音
        public AudioSource musicAudio;//背景音
        public AudioSource voiceAudio;//角色音
        private void Awake()
        {
            Instance = this;
        }
        //播放特效音
        public void PlaySound(string soundpath)
        {
            AudioClip audioClip = Resources.Load<AudioClip>("Aedio/sound/" + soundpath);
            //用于提示有无获取正确音频路径
            if (audioClip != null)
            {
                soundAudio.PlayOneShot(audioClip);
            }
            else
            {
                Debug.LogError("Failed to load audio clip: " + soundpath);
            }
        }
        /// <summary>
        /// 播放背景音
        /// </summary>
        /// <param name="musicpath"></param>
        /// <param name="loop">是否循环播放</param>
        public void PlayMusic(string musicpath, bool loop = true)
        {
            musicAudio.loop = loop;
            musicAudio.Stop();
            musicAudio.PlayOneShot(Resources.Load<AudioClip>("Aedio/music/" + musicpath));
            musicAudio.Play();
        }
        public void StopMusic()
        {
            musicAudio.Stop();
        }
        //播放角色音
        public void PlayVoice(string voicepath)
        {
            voiceAudio.Stop();
            voiceAudio.PlayOneShot(Resources.Load<AudioClip>("Aedio/voice/" + voicepath));
        }
        public void StopVoice()
        {
            voiceAudio.Stop();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
