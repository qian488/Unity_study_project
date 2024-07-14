using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gal_first
{
    //UI管理
    public class UIManger : MonoBehaviour
    {
        public static UIManger Instance { get; private set; }
        public Image imageBG;
        public Image imageCharacter1;
        public Image imageCharacter2;
        public Text text_name;
        public Text text_TalkLine;
        public GameObject TalkLineGod;//对话框父对象游戏物体
        public Transform[] characterPosTrans;
        public Text TextEnergyValue;
        public Text TextFavorValue;
        public GameObject EmpChoiceUIGO;//多项选择框父对象
        public GameObject[] EmpChoiceUIGOS;
        public Text[] TextChoiceUI;
        public GameObject ImageBGGod;//背景显示父对象
        public GameObject ImageCharcaterGod;//人物显示父对象


        private void Awake()
        {
            Instance = this;

        }

        //设置背景图片的方法
        public void SetBGImageSprite(string SpriteName)
        {
            string path = "Image/BG/" + SpriteName;
            Sprite sprite = Resources.Load<Sprite>(path);
            if (sprite != null)//检验有无正确录入图片路径
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
        /// 显示人物的方法
        /// </summary>
        /// <param name="Name">角色名字</param>
        /// <param name="CharacterID">哪个角色</param>
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
        //更新对话内容
        public void UpdateTalkLineText(string TalkLine)
        {
            ShowORHideTalkLine();
            text_TalkLine.text = TalkLine;

        }
        /// <summary>
        /// 更新人物位置
        /// </summary>
        /// <param name="PosID">角色位置，1.左 2.中 3.右</param>
        /// <param name="ifTurnOver">是否翻转</param>
        /// <param name="CharacterID">哪个角色</param>
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
        /// 设置人物位置
        /// </summary>
        /// <param name="PosID">角色位置，1.左 2.中 3.右</param>
        /// <param name="imgTargetCharacter">角色图像</param>
        /// <param name="ifTurnOver">是否翻转</param>
        public void SetPos(int PosID, Image imgTargetCharacter, bool ifTurnOver = false)
        {
            //PosID-1防止数组越界
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
        /// 更新精力值UI
        /// </summary>
        /// <param name="Value">变化值</param>
        public void UpdateEnergyValue(int Value = 0)
        {
            TextEnergyValue.text = Value.ToString();
        }
        /// <summary>
        /// 更新好感度UI
        /// </summary>
        /// <param name="Value">变化值</param>
        /// <param name="Name">好感度对应角色名字</param>
        public void UpdateFavorValue(int Value = 0, string Name = null)
        {
            TextFavorValue.text = Value.ToString();
        }
        /// <summary>
        /// 设置选择对话框
        /// </summary>
        /// <param name="ChoiceNum">多少个选择项</param>
        /// <param name="ChoiceContent">选项内容</param>
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
        //底部对话框显示与隐藏
        public void ShowORHideTalkLine(bool show = true)
        {
            TalkLineGod.SetActive(show);
        }

    }
}

