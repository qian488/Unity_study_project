using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Running
{
    public class Running_GameManager : MonoBehaviour
    {
        public enum GameState
        {
            Start,
            Stop,
        }
        public GameState state = GameState.Stop;
        public GameObject[] Runway;//�����ܵ� 
        public GameObject Azone;//A��Ϊ����ͷ��������,B��Ϊ����������
        public GameObject Bzone;
        public Vector3 AzonePos;//�����ʼλ��
        public Vector3 BzonePos;
        public Button StartBtn;//��ʼ��Ϸ�İ�ť
        public Button StopBtn;//��Ϸ��ͣ��ť
        public Text GoldText;//���ֽ����
        public Text MeterText;//��������
        public Transform Score;//�������ܳɼ�
        public float Speed;
        private float time;
        public float SkydeltaIndex;//����ƶ����
        private Player player;
        private int gold;
        private int meter;
        //��ȡ��Ƶ���
        public AudioSource audioSource;
        public AudioClip[] audioClips;

        // Start is called before the first frame update
        void Start()
        {
            StartBtn.onClick.AddListener(StartGame);
            StopBtn.onClick.AddListener(PauseGame);
            AzonePos = Azone.transform.position;
            BzonePos = Bzone.transform.position;
            player = GameObject.Find("03_Player").GetComponent<Player>();
        }
        //�˳���Ϸ
        public void PauseGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        private void StartGame()
        {
            if (state == GameState.Stop)
            {
                state = GameState.Start;
                StartBtn.gameObject.SetActive(false);
                Score.gameObject.SetActive(false);
                player.initialize();
            }
        }

        public void AddGold(int Count)
        {
            gold += Count;
            GoldText.text = gold.ToString();
            //���Ž����Ч
            PlayAudio(0);
        }
        public void AddMeter(int Count)
        {
            meter += 10 * Count;
            MeterText.text = meter.ToString();
        }
        // Update is called once per frame
        void Update()
        {
            if (state == GameState.Start)
            {
                OnMove();
                time += Time.deltaTime;
                if (time >= 0.2f)
                {
                    time = 0;
                    AddMeter(1);
                }
                transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(SkydeltaIndex * Time.deltaTime, 0);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
        //�ܵ��ƶ�
        private void OnMove()
        {
            Azone.transform.Translate(Vector3.left * Speed * Time.deltaTime, Space.World);
            Bzone.transform.Translate(Vector3.left * Speed * Time.deltaTime, Space.World);
            if (Bzone.transform.position.x <= AzonePos.x)
            {
                Destroy(Azone);
                OnMake();
            }
        }
        //�����µ��ܵ�
        private void OnMake()
        {
            Azone = Bzone;
            Bzone = GameObject.Instantiate(Runway[UnityEngine.Random.Range(0, Runway.Length)]);
            Bzone.transform.position = BzonePos;
        }
        //������Ϸ
        public void StopGame()
        {
            if (state == GameState.Start)
            {
                //����������Ч
                PlayAudio(1);
                //״̬��Ϊֹͣ
                state = GameState.Stop;
                //��ʾ�ɼ�UI
                Score.gameObject.SetActive(true);
                Score.transform.Find("Text _goldscore").GetComponent<Text>().text = gold.ToString();
                Score.transform.Find("Text _meterscore").GetComponent<Text>().text = meter.ToString();
                //�ݻ��ܵ�
                Destroy(Azone);
                Destroy(Bzone);
                //A��B������λ��
                Azone = GameObject.Instantiate(Runway[Runway.Length - 1]);
                Bzone = GameObject.Instantiate(Runway[Runway.Length - 1]);
                Azone.transform.position = AzonePos;
                Bzone.transform.position = BzonePos;
                //���ý�������
                gold = 0;
                meter = 0;
                GoldText.text = gold.ToString();
                MeterText.text = meter.ToString();
                //������ʼ��ť
                StartBtn.gameObject.SetActive(true);
                //�ñ����ص����״̬
                transform.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, 0.01f);
            }
        }
        /// <summary>
        /// ������Ƶ
        /// </summary>
        /// <param name="Index"></param>
        public void PlayAudio(int Index)
        {
            audioSource.clip = audioClips[Index];
            audioSource.Play();
        }
    }

}
