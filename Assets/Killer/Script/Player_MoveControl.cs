using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Killer
{
    public class Player_MoveControl : MonoBehaviour
    {
        public float PlayerSpeed;
        public CharacterController CharacterController;

        public float gravity = -9.81f;
        public Vector3 gravity_Vector3;
        public Transform gravity_transform;
        public float gravity_radius = 0.5f;
        public LayerMask gravity_layerMask;
        private bool isGround;

        public float Height = 100f;
        // Start is called before the first frame update
        void Start()
        {
            if (CharacterController == null)
            {
                CharacterController = CharacterController.GetComponent<CharacterController>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            PlayerMove();
            Gravity();
            if (gravity_Vector3.y == -30)
            {
                //貌似无法实现退出unity编辑器
                Application.Quit();
            }
        }
        void PlayerMove()
        {
            /*
             * transform方法
             一
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 move = transform.forward * PlayerSpeed * Time.deltaTime;
                transform.position += move;
            }
            if···
            二
            float move_x = Input.GetAxis("Horizontal");
            float move_y = Input.GetAxis("Vertical");
            Vector3 move = transform.right * move_x + transform.forward * move_y;
            transform.Translate(move*PlayerSpeed*Time.deltaTime,Space.World);
            */
            float move_x = Input.GetAxis("Horizontal");
            float move_y = Input.GetAxis("Vertical");
            Vector3 move = transform.right * move_x * PlayerSpeed * Time.deltaTime + transform.forward * move_y * PlayerSpeed * Time.deltaTime;
            CharacterController.Move(move);
            //Character controler 方法
        }
        void Gravity()
        {
            gravity_Vector3.y += gravity;
            isGround = Physics.CheckSphere(gravity_transform.position, gravity_radius, gravity_layerMask);
            CharacterController.Move(gravity_Vector3);
            if (isGround == true)
            {
                gravity_Vector3.y = -0.01f;
            }
            if (isGround == true && Input.GetButtonDown("Jump"))
            {
                CharacterController.Move(transform.up * Height);
                Debug.Log("跳");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                //？
                Application.Quit();
                Debug.Log("重新开始");
            }
        }
    }

}

