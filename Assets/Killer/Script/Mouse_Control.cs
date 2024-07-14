using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Killer
{
    public class Mouse_Control : MonoBehaviour
    {
        [Header("物体")]
        public Transform PlayerObject;
        public Transform PlayerCamera;
        [Space]
        public float Rotation_min = -75;
        public float Rotation_max = 75;
        [Range(1f, 200f)]//滑动条
        public float MouseSpeed = 100f;
        float PlayerRotation_Y = 0f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            PlayerMouseControl();
        }
        void PlayerMouseControl()
        {
            float xMouse = Input.GetAxis("Mouse X") * MouseSpeed * Time.deltaTime;//从鼠标获取移动坐标
            float yMouse = Input.GetAxis("Mouse Y") * MouseSpeed * Time.deltaTime;
            //水平移动的实现是x轴坐标的变化，是随着录入的数据每帧改变
            //上下移动的实现是y轴坐标变化的积累，要的是每帧移动后的结果
            PlayerRotation_Y -= yMouse;//集合每一帧y轴坐标的变化
            PlayerRotation_Y = Mathf.Clamp(PlayerRotation_Y, Rotation_min, Rotation_max);//限制俯仰的最大限度

            PlayerObject.Rotate(Vector3.up * xMouse);

            //欧拉角转换为四元数
            Quaternion quaternion = Quaternion.Euler(PlayerRotation_Y, 0, 0);
            PlayerCamera.localRotation = quaternion;
        }
    }
}

