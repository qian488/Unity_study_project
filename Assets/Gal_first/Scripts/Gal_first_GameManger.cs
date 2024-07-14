using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gal_first
{
    //��Ϸ����
    public class Gal_first_GameManger : MonoBehaviour
    {
        public static Gal_first_GameManger Instance { get; private set; }
        public List<ScriptData> scriptdatas;
        private int scriptIndex;
        public int EnergyValue;//����ֵ״̬
        public Dictionary<string, int> FavorabilityDict;//ÿ����ɫ����ҵĺøж�

        private void Awake()
        {
            Instance = this;
        }
        public void Start()
        {
            // ��������г�ʼ��������
            Debug.Log("�ű���������");
            scriptdatas = new List<ScriptData>()
        {
            new ScriptData()
            {
                LoadType=1,SpirteName="��_Ŀ�i��ͨ��A",
                SoundType=3,SoundPath="AI music_5"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=2,Name="Sugar",CharacterID=1,Energy=10,
                SoundType=1,SoundPath="Character01_1",
                TalkLine="Hello! My name is Sugar!"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Sugar",CharacterID=1,Favorability=5,
                SoundType=1,SoundPath="Character01_2",
                TalkLine="�š��Ų��ǹ���˵Ӣ��ߣ�"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=1,ifTurnOver=true,Name="Sugar",CharacterID=1,
                SoundType=1,SoundPath="Character01_3",
                TalkLine="ciallo!!!~~~"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=1,ifTurnOver=true,Name="Sugar",CharacterID=1,Energy=-10,
                SoundType=1,SoundPath="Character01_4",
                TalkLine="û���Ҿ������ӳ���"
            },
            new ScriptData()
            {
                SoundType=3,SoundPath="AI music_11"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,
                SoundType=1,SoundPath="Character02_1",
                TalkLine="��ã�����Debug�����ָ�̡�"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=1,ifTurnOver=true,Name="Sugar",CharacterID=1,
                SoundType=1,SoundPath="Character01_5",
                TalkLine="������ô���㰡��û��˼��"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,
                SoundType=1,SoundPath="Character02_2",
                TalkLine="����˵Ц�ˡ�"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=1,ifTurnOver=true,Name="Sugar",CharacterID=1,
                SoundType=1,SoundPath="Character01_6",
                TalkLine="����˵������ȻС�������㡣"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,
                SoundType=1,SoundPath="Character02_3",
                TalkLine="�����У������㡣��˵�����������벻�������Ի�����"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=1,ifTurnOver=true,Name="Sugar",CharacterID=1,
                SoundType=1,SoundPath="Character01_7",
                TalkLine="�������������������������ˡ�"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,
                SoundType=1,SoundPath="Character02_4",
                TalkLine="ϰ�߾ͺã�ϰ�߾ͺá�"
            },
            new ScriptData()
            {
                LoadType = 3,EventID=1,EventData=3
            },
            new ScriptData()
            {
                LoadType=3,EventID=2,EventData=2,
                TalkLine="��...���������������𣿲����ʰɡ�"
            },
            new ScriptData()
            {
                LoadType=3,EventID=2,EventData=3,
                TalkLine="������˭�����������"
            },
            new ScriptData()
            {
                LoadType=3,EventID=2,EventData=4,
                TalkLine="��׼�����ˡ�"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,ScriptID=2,
                SoundType=1,SoundPath="Character02_5",
                TalkLine="��Ȼ����������ߣ���������"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,
                TalkLine="������"
            },
            new ScriptData()
            {
                LoadType = 3,EventID=2,EventData=4
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,ScriptID=3,
                SoundType=1,SoundPath="Character02_7",
                TalkLine="������Ľ�����ר�ŵ��彣��"
            },
            new ScriptData()
            {
                LoadType = 3,EventID=2,EventData=4
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,ScriptID=4,
                SoundType=1,SoundPath="Character02_6",
                TalkLine="ȥ�����硣"
            }
        };
            scriptIndex = 0;
            HandData();
            EnergyValue = 100;
            ChangeEnergyValue();//������ֵ��ʼ��
            FavorabilityDict = new Dictionary<string, int>()
        {
            {"Player",0},
            {"Sugar",60 },
            {"Debug",60 }
        };
            for (int i = 0; i < scriptdatas.Count; i++)
            {
                scriptdatas[i].scriptIndex = i;
            }
        }
        //����ÿһ����������
        private void HandData()
        {
            Debug.Log(scriptIndex);
            Debug.Log(scriptdatas.Count);
            if (scriptIndex >= scriptdatas.Count)
            {
                Debug.Log("Game over��");
                return;
            }
            PlayMusical(scriptdatas[scriptIndex].SoundType);
            Debug.Log("HandData���гɹ�");
            if (scriptdatas[scriptIndex].LoadType == 1)
            {
                Debug.Log("LoadType==1");
                //����
                //���ñ���ͼƬ
                SetBGImageSprite(scriptdatas[scriptIndex].SpirteName);
                //������һ����������
                LoadNextScript();

            }
            else if (scriptdatas[scriptIndex].LoadType == 2)
            {
                //����
                HandleCharacter();
            }
            else if (scriptdatas[scriptIndex].LoadType == 3)
            {
                //�¼�
                Debug.Log(scriptIndex);
                switch (scriptdatas[scriptIndex].EventID)
                {
                    //��ʾ����ѡ��
                    case 1:
                        ShowChoiceUI(scriptdatas[scriptIndex].EventData, GetChoiceContent(scriptdatas[scriptIndex].EventData));
                        break;
                    //��ת�����λ�þ籾
                    case 2:
                        SetScriptIndex();
                        break;

                    default:
                        break;
                }
            }
            else
            {
                LoadNextScript();
            }
        }
        //���ñ���ͼƬ
        private void SetBGImageSprite(string SpirteName)
        {
            UIManger.Instance.SetBGImageSprite(SpirteName);
        }
        //������һ����������
        public void LoadNextScript()
        {
            Debug.Log("������һ������");
            scriptIndex++;
            Debug.Log(scriptIndex);
            HandData();
        }
        //��ʾ����
        private void ShowCharacters(string Name, int CharacterID = 1)
        {
            UIManger.Instance.ShowCharacter(Name, CharacterID);
        }
        //���¶Ի����ı�
        private void UpdateTalkLineText(string TalkLine)
        {
            UIManger.Instance.UpdateTalkLineText(TalkLine);
        }
        //��������λ��
        private void SetcharacterPos(int PosID, bool ifTurnOver = false, int CharacterID = 1)
        {
            UIManger.Instance.SetcharacterPos(PosID, ifTurnOver, CharacterID);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="SoundType">��Ƶ���ͣ�1.��ɫ�� 2.��Ч�� 3.������</param>
        public void PlayMusical(int SoundType)
        {
            switch (SoundType)
            {
                case 1:
                    AudiosourceManger.Instance.PlayVoice(
                        scriptdatas[scriptIndex].Name + "/" + scriptdatas[scriptIndex].SoundPath);
                    break;
                case 2:
                    AudiosourceManger.Instance.PlaySound(scriptdatas[scriptIndex].SoundPath);
                    break;
                case 3:
                    AudiosourceManger.Instance.PlayMusic(scriptdatas[scriptIndex].SoundPath);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// �ı侫��ֵ
        /// </summary>
        /// <param name="Value">������Ҫ�ı���٣�����</param>
        public void ChangeEnergyValue(int Value = 0)
        {

            if (Value == 0)
            {
                UpdateEnergyValue(EnergyValue);//��ֹ�����仯ֵΪ0�����ɸ��¾���ֵ
                return;
            }
            if (Value > 0)
            {
                AudiosourceManger.Instance.PlaySound("AI music_sound_1");
            }
            EnergyValue += Value;
            if (EnergyValue > 100)
            {
                EnergyValue = 100;
            }
            else if (EnergyValue < 0)
            {
                EnergyValue = 0;
            }
            UpdateEnergyValue(EnergyValue);
        }
        //���¾���ֵUI
        public void UpdateEnergyValue(int Value = 0)
        {
            UIManger.Instance.UpdateEnergyValue(Value);
        }
        /// <summary>
        /// �ı�øж�
        /// </summary>
        /// <param name="Value">�øжȱ仯ֵ</param>
        /// <param name="Name">�øжȶ�Ӧ��ɫ</param>
        public void ChangeFavorValue(int Value, string Name = null)
        {
            if (Value == 0)
            {
                return;
            }
            if (Value > 0)
            {
                AudiosourceManger.Instance.PlaySound("AI music_sound_2");
            }
            FavorabilityDict[Name] += Value;
            if (FavorabilityDict[Name] > 200)
            {
                FavorabilityDict[Name] = 200;
            }
            else if (FavorabilityDict[Name] < 0)
            {
                FavorabilityDict[Name] = 0;
            }
            UpdateFavorValue(FavorabilityDict[Name], Name);
        }
        //���ºøж�UI
        public void UpdateFavorValue(int Value = 0, string Name = null)
        {
            UIManger.Instance.UpdateFavorValue(Value, Name);
        }
        //����������ص�����
        public void HandleCharacter(bool showCharacterOnly = false)
        {
            //��ʾ����
            ShowCharacters(scriptdatas[scriptIndex].Name, scriptdatas[scriptIndex].CharacterID);

            //��������λ��
            SetcharacterPos(scriptdatas[scriptIndex].characterPos, scriptdatas[scriptIndex].ifTurnOver, scriptdatas[scriptIndex].CharacterID);
            if (!showCharacterOnly)
            {
                //���¶Ի����ı�
                UpdateTalkLineText(scriptdatas[scriptIndex].TalkLine);
                //�ı����ﾫ��ֵ�ͺøж�
                ChangeEnergyValue(scriptdatas[scriptIndex].Energy);
                ChangeFavorValue(scriptdatas[scriptIndex].Favorability, scriptdatas[scriptIndex].Name);
            }

        }
        // ��ʾ����ѡ���
        public void ShowChoiceUI(int choicenum, string[] choicecontent)
        {
            UIManger.Instance.ShowChoiceUI(choicenum, choicecontent);
        }
        /// <summary>
        /// ���»�ȡ�籾
        /// </summary>
        /// <param name="Num">��ȡ����</param>
        /// <returns></returns>
        public string[] GetChoiceContent(int Num)
        {
            string[] choicecontent = new string[Num];
            for (int i = 0; i < Num; i++)
            {
                choicecontent[i] = scriptdatas[scriptIndex + i + 1].TalkLine;
            }
            return choicecontent;
        }
        /// <summary>
        /// ���þ籾����
        /// </summary>
        /// <param name="index"></param>
        public void SetScriptIndex(int index = 0)
        {
            for (int i = 0; i < scriptdatas.Count; i++)
            {
                Debug.Log(scriptIndex + index);
                Debug.Log(i);
                if (scriptdatas[scriptIndex + index].EventData == scriptdatas[i].ScriptID)
                {
                    scriptIndex = scriptdatas[i].scriptIndex;
                    Debug.Log("�ҵ���ת�ľ籾����");
                    break;
                }
            }
            HandData();
        }
    }
}

