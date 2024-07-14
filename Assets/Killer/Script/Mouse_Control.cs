using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Killer
{
    public class Mouse_Control : MonoBehaviour
    {
        [Header("����")]
        public Transform PlayerObject;
        public Transform PlayerCamera;
        [Space]
        public float Rotation_min = -75;
        public float Rotation_max = 75;
        [Range(1f, 200f)]//������
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
            float xMouse = Input.GetAxis("Mouse X") * MouseSpeed * Time.deltaTime;//������ȡ�ƶ�����
            float yMouse = Input.GetAxis("Mouse Y") * MouseSpeed * Time.deltaTime;
            //ˮƽ�ƶ���ʵ����x������ı仯��������¼�������ÿ֡�ı�
            //�����ƶ���ʵ����y������仯�Ļ��ۣ�Ҫ����ÿ֡�ƶ���Ľ��
            PlayerRotation_Y -= yMouse;//����ÿһ֡y������ı仯
            PlayerRotation_Y = Mathf.Clamp(PlayerRotation_Y, Rotation_min, Rotation_max);//���Ƹ���������޶�

            PlayerObject.Rotate(Vector3.up * xMouse);

            //ŷ����ת��Ϊ��Ԫ��
            Quaternion quaternion = Quaternion.Euler(PlayerRotation_Y, 0, 0);
            PlayerCamera.localRotation = quaternion;
        }
    }
}

