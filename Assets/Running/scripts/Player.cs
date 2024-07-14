using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Running
{
    public class Player : MonoBehaviour
    {
        //人物状态
        public enum PlayerState
        {
            Run,//跑
            Jump,//跳
            DoubleJump,//连跳
            Die//死亡
        }
        //跳跃的力度
        public float Jump_Power;
        //跑、跳、连跳动画贴图
        public Texture[] RunTexture;
        public Texture[] JumpTexture;
        public Texture[] DoubleJumpTexture;
        //当前播放到那一张贴图
        private int RunIndex;
        private int JumpIndex;
        private int DoubleJumpIndex;
        //角色渲染器组件
        Renderer PlayerRenderer;
        private Vector3 BirthPos;//初始位置
                                 //角色的刚体组件
        private Rigidbody rig;
        //角色当前状态
        private PlayerState playerState;
        //角色播放动画的速度，也就是切换图片的时间间隔
        public float AniPlaySpeed;
        public float time;
        //是否播放跑、跳、连跳的动画
        private bool isPlayerRun;
        private bool isPlayerJump;
        private bool isPlayerDoubleJump;
        private Running_GameManager gameManager;


        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameObject.Find("01_Sky").GetComponent<Running_GameManager>();
            rig = transform.GetComponent<Rigidbody>();//获取角色刚体组件
            PlayerRenderer = transform.Find("Sprite_Plane").GetComponent<Renderer>();//获取角色渲染器组件
            BirthPos = transform.position;//获取初始位置
            rig.Sleep();//避免还没开始角色就降落

        }
        //角色初始化的方法
        public void initialize()
        {
            playerState = PlayerState.Run;
            //播放跑的动画
            PlayerRun();
            transform.position = BirthPos;//将位置重置
            rig.useGravity = true;
            rig.WakeUp();
        }
        // Update is called once per frame
        void Update()
        {
            if (gameManager.state == Running_GameManager.GameState.Start && playerState != PlayerState.Die)
            {
                CheckDie();
                rig.WakeUp();
                time += Time.deltaTime;
                if (Input.GetKeyDown("space"))//如果按下空格键，根据人物状态切换跳和连跳
                {
                    if (playerState == PlayerState.Run)//如果是跑，切换到跳
                    {
                        playerState = PlayerState.Jump;
                        transform.GetComponent<Rigidbody>().AddForce(0, Jump_Power * 1.5f, 0);
                        PlayerJump();
                        gameManager.PlayAudio(2); //播放跳跃音效
                    }
                    else if (playerState == PlayerState.Jump)//如果是跳，切换到连跳
                    {
                        playerState = PlayerState.DoubleJump;
                        transform.GetComponent<Rigidbody>().AddForce(0, Jump_Power, 0);
                        PlayerDoubleJump();
                        gameManager.PlayAudio(2); //播放跳跃音效
                    }
                }
                if (time >= AniPlaySpeed)
                {
                    time = 0;
                    if (isPlayerRun == true)
                    {
                        RunAniIng();
                    }
                    if (isPlayerJump == true)
                    {
                        JumpAniIng();
                    }
                    if (isPlayerDoubleJump == true)
                    {
                        DoubleJumpAniIng();
                    }
                }
            }

        }
        //检测角色死亡
        public void CheckDie()
        {
            if (transform.position.x <= -15 || transform.position.y <= -15)
            {
                playerState = PlayerState.Die;
                rig.useGravity = false;
                rig.Sleep();
                gameManager.StopGame();
            }
        }
        //进入跑的动画
        private void PlayerRun()
        {
            isPlayerRun = true;
            isPlayerJump = false;
            isPlayerDoubleJump = false;
            RunIndex = 0;
        }
        //进入跳的动画
        private void PlayerJump()
        {
            isPlayerRun = false;
            isPlayerJump = true;
            isPlayerDoubleJump = false;
            JumpIndex = 0;
        }
        //进入连跳的动画
        private void PlayerDoubleJump()
        {
            isPlayerRun = false;
            isPlayerJump = false;
            isPlayerDoubleJump = true;
            DoubleJumpIndex = 0;
        }
        //跑的动画中
        private void RunAniIng()
        {
            RunIndex += 1;
            if (RunIndex >= RunTexture.Length)
            {
                RunIndex = 0;//循环播放，只需最后回到初始即可
            }
            PlayerRenderer.material.mainTexture = RunTexture[RunIndex];
        }
        //跳的动画中
        private void JumpAniIng()
        {
            JumpIndex += 1;
            if (JumpIndex >= JumpTexture.Length)
            {
                PlayerRun();
                return;
            }
            PlayerRenderer.material.mainTexture = JumpTexture[JumpIndex];
        }
        //连跳的动画中
        private void DoubleJumpAniIng()
        {
            DoubleJumpIndex += 1;
            if (DoubleJumpIndex >= DoubleJumpTexture.Length)
            {
                PlayerRun();
                return;
            }
            PlayerRenderer.material.mainTexture = DoubleJumpTexture[DoubleJumpIndex];
        }
        //进入碰撞的方法
        private void OnCollisionEnter(Collision collision)
        {
            if (gameManager.state == Running_GameManager.GameState.Stop)
            {
                return;
            }
            //如果接地后是跳或者连跳状态，切换为跑
            if (playerState == PlayerState.Jump || playerState == PlayerState.DoubleJump)
            {
                playerState = PlayerState.Run;
            }
        }
        //碰撞金币
        private void OnTriggerEnter(Collider other)
        {
            if (gameManager.state == Running_GameManager.GameState.Stop)
            {
                return;
            }
            if (other.CompareTag("coin"))
            {
                gameManager.AddGold(1);
                Destroy(other.gameObject);
            }
        }
    }
}

