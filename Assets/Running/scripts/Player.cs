using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Running
{
    public class Player : MonoBehaviour
    {
        //����״̬
        public enum PlayerState
        {
            Run,//��
            Jump,//��
            DoubleJump,//����
            Die//����
        }
        //��Ծ������
        public float Jump_Power;
        //�ܡ���������������ͼ
        public Texture[] RunTexture;
        public Texture[] JumpTexture;
        public Texture[] DoubleJumpTexture;
        //��ǰ���ŵ���һ����ͼ
        private int RunIndex;
        private int JumpIndex;
        private int DoubleJumpIndex;
        //��ɫ��Ⱦ�����
        Renderer PlayerRenderer;
        private Vector3 BirthPos;//��ʼλ��
                                 //��ɫ�ĸ������
        private Rigidbody rig;
        //��ɫ��ǰ״̬
        private PlayerState playerState;
        //��ɫ���Ŷ������ٶȣ�Ҳ�����л�ͼƬ��ʱ����
        public float AniPlaySpeed;
        public float time;
        //�Ƿ񲥷��ܡ����������Ķ���
        private bool isPlayerRun;
        private bool isPlayerJump;
        private bool isPlayerDoubleJump;
        private Running_GameManager gameManager;


        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameObject.Find("01_Sky").GetComponent<Running_GameManager>();
            rig = transform.GetComponent<Rigidbody>();//��ȡ��ɫ�������
            PlayerRenderer = transform.Find("Sprite_Plane").GetComponent<Renderer>();//��ȡ��ɫ��Ⱦ�����
            BirthPos = transform.position;//��ȡ��ʼλ��
            rig.Sleep();//���⻹û��ʼ��ɫ�ͽ���

        }
        //��ɫ��ʼ���ķ���
        public void initialize()
        {
            playerState = PlayerState.Run;
            //�����ܵĶ���
            PlayerRun();
            transform.position = BirthPos;//��λ������
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
                if (Input.GetKeyDown("space"))//������¿ո������������״̬�л���������
                {
                    if (playerState == PlayerState.Run)//������ܣ��л�����
                    {
                        playerState = PlayerState.Jump;
                        transform.GetComponent<Rigidbody>().AddForce(0, Jump_Power * 1.5f, 0);
                        PlayerJump();
                        gameManager.PlayAudio(2); //������Ծ��Ч
                    }
                    else if (playerState == PlayerState.Jump)//����������л�������
                    {
                        playerState = PlayerState.DoubleJump;
                        transform.GetComponent<Rigidbody>().AddForce(0, Jump_Power, 0);
                        PlayerDoubleJump();
                        gameManager.PlayAudio(2); //������Ծ��Ч
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
        //����ɫ����
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
        //�����ܵĶ���
        private void PlayerRun()
        {
            isPlayerRun = true;
            isPlayerJump = false;
            isPlayerDoubleJump = false;
            RunIndex = 0;
        }
        //�������Ķ���
        private void PlayerJump()
        {
            isPlayerRun = false;
            isPlayerJump = true;
            isPlayerDoubleJump = false;
            JumpIndex = 0;
        }
        //���������Ķ���
        private void PlayerDoubleJump()
        {
            isPlayerRun = false;
            isPlayerJump = false;
            isPlayerDoubleJump = true;
            DoubleJumpIndex = 0;
        }
        //�ܵĶ�����
        private void RunAniIng()
        {
            RunIndex += 1;
            if (RunIndex >= RunTexture.Length)
            {
                RunIndex = 0;//ѭ�����ţ�ֻ�����ص���ʼ����
            }
            PlayerRenderer.material.mainTexture = RunTexture[RunIndex];
        }
        //���Ķ�����
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
        //�����Ķ�����
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
        //������ײ�ķ���
        private void OnCollisionEnter(Collision collision)
        {
            if (gameManager.state == Running_GameManager.GameState.Stop)
            {
                return;
            }
            //����ӵغ�������������״̬���л�Ϊ��
            if (playerState == PlayerState.Jump || playerState == PlayerState.DoubleJump)
            {
                playerState = PlayerState.Run;
            }
        }
        //��ײ���
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

