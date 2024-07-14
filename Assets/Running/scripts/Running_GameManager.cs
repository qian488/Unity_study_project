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
        public GameObject[] Runway;//缓存跑道 
        public GameObject Azone;//A区为摄像头看见区域,B区为看不见区域
        public GameObject Bzone;
        public Vector3 AzonePos;//区域初始位置
        public Vector3 BzonePos;
        public Button StartBtn;//开始游戏的按钮
        public Button StopBtn;//游戏暂停按钮
        public Text GoldText;//本局金币数
        public Text MeterText;//本局米数
        public Transform Score;//本局最总成绩
        public float Speed;
        private float time;
        public float SkydeltaIndex;//天空移动间隔
        private Player player;
        private int gold;
        private int meter;
        //获取音频组件
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
        //退出游戏
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
            //播放金币音效
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
        //跑道移动
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
        //生成新的跑道
        private void OnMake()
        {
            Azone = Bzone;
            Bzone = GameObject.Instantiate(Runway[UnityEngine.Random.Range(0, Runway.Length)]);
            Bzone.transform.position = BzonePos;
        }
        //结束游戏
        public void StopGame()
        {
            if (state == GameState.Start)
            {
                //播放死亡音效
                PlayAudio(1);
                //状态变为停止
                state = GameState.Stop;
                //显示成绩UI
                Score.gameObject.SetActive(true);
                Score.transform.Find("Text _goldscore").GetComponent<Text>().text = gold.ToString();
                Score.transform.Find("Text _meterscore").GetComponent<Text>().text = meter.ToString();
                //摧毁跑道
                Destroy(Azone);
                Destroy(Bzone);
                //A区B区重置位置
                Azone = GameObject.Instantiate(Runway[Runway.Length - 1]);
                Bzone = GameObject.Instantiate(Runway[Runway.Length - 1]);
                Azone.transform.position = AzonePos;
                Bzone.transform.position = BzonePos;
                //重置金币与距离
                gold = 0;
                meter = 0;
                GoldText.text = gold.ToString();
                MeterText.text = meter.ToString();
                //开启开始按钮
                StartBtn.gameObject.SetActive(true);
                //让背景回到最初状态
                transform.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, 0.01f);
            }
        }
        /// <summary>
        /// 播放音频
        /// </summary>
        /// <param name="Index"></param>
        public void PlayAudio(int Index)
        {
            audioSource.clip = audioClips[Index];
            audioSource.Play();
        }
    }

}
