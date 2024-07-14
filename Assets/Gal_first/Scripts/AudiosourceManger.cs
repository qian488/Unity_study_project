using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gal_first
{
    //���ֹ���
    public class AudiosourceManger : MonoBehaviour
    {
        public static AudiosourceManger Instance { get; private set; }
        public AudioSource soundAudio;//��Ч��
        public AudioSource musicAudio;//������
        public AudioSource voiceAudio;//��ɫ��
        private void Awake()
        {
            Instance = this;
        }
        //������Ч��
        public void PlaySound(string soundpath)
        {
            AudioClip audioClip = Resources.Load<AudioClip>("Aedio/sound/" + soundpath);
            //������ʾ���޻�ȡ��ȷ��Ƶ·��
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
        /// ���ű�����
        /// </summary>
        /// <param name="musicpath"></param>
        /// <param name="loop">�Ƿ�ѭ������</param>
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
        //���Ž�ɫ��
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
