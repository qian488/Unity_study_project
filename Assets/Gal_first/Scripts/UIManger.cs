using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gal_first
{
    //UI����
    public class UIManger : MonoBehaviour
    {
        public static UIManger Instance { get; private set; }
        public Image imageBG;
        public Image imageCharacter1;
        public Image imageCharacter2;
        public Text text_name;
        public Text text_TalkLine;
        public GameObject TalkLineGod;//�Ի��򸸶�����Ϸ����
        public Transform[] characterPosTrans;
        public Text TextEnergyValue;
        public Text TextFavorValue;
        public GameObject EmpChoiceUIGO;//����ѡ��򸸶���
        public GameObject[] EmpChoiceUIGOS;
        public Text[] TextChoiceUI;
        public GameObject ImageBGGod;//������ʾ������
        public GameObject ImageCharcaterGod;//������ʾ������


        private void Awake()
        {
            Instance = this;

        }

        //���ñ���ͼƬ�ķ���
        public void SetBGImageSprite(string SpriteName)
        {
            string path = "Image/BG/" + SpriteName;
            Sprite sprite = Resources.Load<Sprite>(path);
            if (sprite != null)//����������ȷ¼��ͼƬ·��
            {
                imageBG.sprite = sprite;
                ShowBG();
                Debug.Log("Bg");
            }
            else
            {
                Debug.LogError("Failed to load sprite: " + path);
            }
        }
        public void ShowBG(bool show = true)
        {
            ImageBGGod.SetActive(show);
        }
        /// <summary>
        /// ��ʾ����ķ���
        /// </summary>
        /// <param name="Name">��ɫ����</param>
        /// <param name="CharacterID">�ĸ���ɫ</param>
        public void ShowCharacter(string Name, int CharacterID = 1)
        {
            CloseChoiceUI();
            text_name.text = Name;
            ShowORHideTalkLine(false);
            ShowCharacter();
            if (CharacterID == 1)
            {
                imageCharacter1.sprite = Resources.Load<Sprite>("Image/Character/" + Name);
                imageCharacter1.SetNativeSize();
                imageCharacter1.gameObject.SetActive(true);
            }
            else if (CharacterID == 2)
            {
                imageCharacter2.sprite = Resources.Load<Sprite>("Image/Character/" + Name);
                imageCharacter2.SetNativeSize();
                imageCharacter2.gameObject.SetActive(true);
            }
        }
        public void ShowCharacter(bool show = true)
        {
            ImageCharcaterGod.SetActive(show);
        }
        //���¶Ի�����
        public void UpdateTalkLineText(string TalkLine)
        {
            ShowORHideTalkLine();
            text_TalkLine.text = TalkLine;

        }
        /// <summary>
        /// ��������λ��
        /// </summary>
        /// <param name="PosID">��ɫλ�ã�1.�� 2.�� 3.��</param>
        /// <param name="ifTurnOver">�Ƿ�ת</param>
        /// <param name="CharacterID">�ĸ���ɫ</param>
        public void SetcharacterPos(int PosID, bool ifTurnOver = false, int CharacterID = 1)
        {
            if (CharacterID == 1)
            {
                SetPos(PosID, imageCharacter1, ifTurnOver);
            }
            else if (CharacterID == 2)
            {
                SetPos(PosID, imageCharacter2, ifTurnOver);
            }
        }
        /// <summary>
        /// ��������λ��
        /// </summary>
        /// <param name="PosID">��ɫλ�ã�1.�� 2.�� 3.��</param>
        /// <param name="imgTargetCharacter">��ɫͼ��</param>
        /// <param name="ifTurnOver">�Ƿ�ת</param>
        public void SetPos(int PosID, Image imgTargetCharacter, bool ifTurnOver = false)
        {
            //PosID-1��ֹ����Խ��
            imgTargetCharacter.transform.localPosition = characterPosTrans[PosID - 1].localPosition;
            if (ifTurnOver)
            {
                imgTargetCharacter.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                imgTargetCharacter.transform.eulerAngles = Vector3.zero;
            }
        }
        /// <summary>
        /// ���¾���ֵUI
        /// </summary>
        /// <param name="Value">�仯ֵ</param>
        public void UpdateEnergyValue(int Value = 0)
        {
            TextEnergyValue.text = Value.ToString();
        }
        /// <summary>
        /// ���ºøж�UI
        /// </summary>
        /// <param name="Value">�仯ֵ</param>
        /// <param name="Name">�øжȶ�Ӧ��ɫ����</param>
        public void UpdateFavorValue(int Value = 0, string Name = null)
        {
            TextFavorValue.text = Value.ToString();
        }
        /// <summary>
        /// ����ѡ��Ի���
        /// </summary>
        /// <param name="ChoiceNum">���ٸ�ѡ����</param>
        /// <param name="ChoiceContent">ѡ������</param>
        public void ShowChoiceUI(int ChoiceNum, string[] ChoiceContent)
        {
            EmpChoiceUIGO.SetActive(true);
            ShowORHideTalkLine(false);
            for (int i = 0; i < EmpChoiceUIGOS.Length; i++)
            {
                EmpChoiceUIGOS[i].SetActive(false);
            }
            for (int i = 0; i < ChoiceNum; i++)
            {
                EmpChoiceUIGOS[i].SetActive(true);
                TextChoiceUI[i].text = ChoiceContent[i];
            }
        }
        public void CloseChoiceUI()
        {
            EmpChoiceUIGO.SetActive(false);
        }
        //�ײ��Ի�����ʾ������
        public void ShowORHideTalkLine(bool show = true)
        {
            TalkLineGod.SetActive(show);
        }

    }
}

