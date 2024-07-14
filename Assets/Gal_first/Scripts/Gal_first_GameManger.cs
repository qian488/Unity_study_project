using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gal_first
{
    //游戏主控
    public class Gal_first_GameManger : MonoBehaviour
    {
        public static Gal_first_GameManger Instance { get; private set; }
        public List<ScriptData> scriptdatas;
        private int scriptIndex;
        public int EnergyValue;//精力值状态
        public Dictionary<string, int> FavorabilityDict;//每个角色对玩家的好感度

        private void Awake()
        {
            Instance = this;
        }
        public void Start()
        {
            // 在这里进行初始化或设置
            Debug.Log("脚本已启动！");
            scriptdatas = new List<ScriptData>()
        {
            new ScriptData()
            {
                LoadType=1,SpirteName="街_目抜き通りA",
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
                TalkLine="才、才不是故意说英语，哼！"
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
                TalkLine="没错！我就是柚子厨！"
            },
            new ScriptData()
            {
                SoundType=3,SoundPath="AI music_11"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,
                SoundType=1,SoundPath="Character02_1",
                TalkLine="你好，在下Debug，请多指教。"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=1,ifTurnOver=true,Name="Sugar",CharacterID=1,
                SoundType=1,SoundPath="Character01_5",
                TalkLine="啊，怎么是你啊？没意思。"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,
                SoundType=1,SoundPath="Character02_2",
                TalkLine="姑娘说笑了。"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=1,ifTurnOver=true,Name="Sugar",CharacterID=1,
                SoundType=1,SoundPath="Character01_6",
                TalkLine="正常说话！不然小心我揍你。"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,
                SoundType=1,SoundPath="Character02_3",
                TalkLine="行行行，都依你。话说，新生，你想不想来试试击剑？"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=1,ifTurnOver=true,Name="Sugar",CharacterID=1,
                SoundType=1,SoundPath="Character01_7",
                TalkLine="你再这样，新生都给你吓跑了。"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,
                SoundType=1,SoundPath="Character02_4",
                TalkLine="习惯就好，习惯就好。"
            },
            new ScriptData()
            {
                LoadType = 3,EventID=1,EventData=3
            },
            new ScriptData()
            {
                LoadType=3,EventID=2,EventData=2,
                TalkLine="击...击剑？在这里面吗？不合适吧。"
            },
            new ScriptData()
            {
                LoadType=3,EventID=2,EventData=3,
                TalkLine="正常人谁随身带剑啊！"
            },
            new ScriptData()
            {
                LoadType=3,EventID=2,EventData=4,
                TalkLine="我准备好了。"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,ScriptID=2,
                SoundType=1,SoundPath="Character02_5",
                TalkLine="当然不是在这里，走，跟我来。"
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,
                TalkLine="？？？"
            },
            new ScriptData()
            {
                LoadType = 3,EventID=2,EventData=4
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,ScriptID=3,
                SoundType=1,SoundPath="Character02_7",
                TalkLine="动漫社的教室有专门的佩剑。"
            },
            new ScriptData()
            {
                LoadType = 3,EventID=2,EventData=4
            },
            new ScriptData()
            {
                LoadType=2,characterPos=3,Name="Debug",CharacterID=2,ScriptID=4,
                SoundType=1,SoundPath="Character02_6",
                TalkLine="去动漫社。"
            }
        };
            scriptIndex = 0;
            HandData();
            EnergyValue = 100;
            ChangeEnergyValue();//将精力值初始化
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
        //处理每一条剧情数据
        private void HandData()
        {
            Debug.Log(scriptIndex);
            Debug.Log(scriptdatas.Count);
            if (scriptIndex >= scriptdatas.Count)
            {
                Debug.Log("Game over！");
                return;
            }
            PlayMusical(scriptdatas[scriptIndex].SoundType);
            Debug.Log("HandData运行成功");
            if (scriptdatas[scriptIndex].LoadType == 1)
            {
                Debug.Log("LoadType==1");
                //背景
                //设置背景图片
                SetBGImageSprite(scriptdatas[scriptIndex].SpirteName);
                //加载下一条剧情数据
                LoadNextScript();

            }
            else if (scriptdatas[scriptIndex].LoadType == 2)
            {
                //人物
                HandleCharacter();
            }
            else if (scriptdatas[scriptIndex].LoadType == 3)
            {
                //事件
                Debug.Log(scriptIndex);
                switch (scriptdatas[scriptIndex].EventID)
                {
                    //显示多项选择
                    case 1:
                        ShowChoiceUI(scriptdatas[scriptIndex].EventData, GetChoiceContent(scriptdatas[scriptIndex].EventData));
                        break;
                    //跳转到标记位置剧本
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
        //设置背景图片
        private void SetBGImageSprite(string SpirteName)
        {
            UIManger.Instance.SetBGImageSprite(SpirteName);
        }
        //加载下一条剧情数据
        public void LoadNextScript()
        {
            Debug.Log("加载下一条剧情");
            scriptIndex++;
            Debug.Log(scriptIndex);
            HandData();
        }
        //显示人物
        private void ShowCharacters(string Name, int CharacterID = 1)
        {
            UIManger.Instance.ShowCharacter(Name, CharacterID);
        }
        //更新对话框文本
        private void UpdateTalkLineText(string TalkLine)
        {
            UIManger.Instance.UpdateTalkLineText(TalkLine);
        }
        //更新人物位置
        private void SetcharacterPos(int PosID, bool ifTurnOver = false, int CharacterID = 1)
        {
            UIManger.Instance.SetcharacterPos(PosID, ifTurnOver, CharacterID);
        }
        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="SoundType">音频类型，1.角色音 2.特效音 3.背景音</param>
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
        /// 改变精力值
        /// </summary>
        /// <param name="Value">传入需要改变多少，即Δ</param>
        public void ChangeEnergyValue(int Value = 0)
        {

            if (Value == 0)
            {
                UpdateEnergyValue(EnergyValue);//防止出错，变化值为0，依旧更新精力值
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
        //更新精力值UI
        public void UpdateEnergyValue(int Value = 0)
        {
            UIManger.Instance.UpdateEnergyValue(Value);
        }
        /// <summary>
        /// 改变好感度
        /// </summary>
        /// <param name="Value">好感度变化值</param>
        /// <param name="Name">好感度对应角色</param>
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
        //更新好感度UI
        public void UpdateFavorValue(int Value = 0, string Name = null)
        {
            UIManger.Instance.UpdateFavorValue(Value, Name);
        }
        //处理人物相关的内容
        public void HandleCharacter(bool showCharacterOnly = false)
        {
            //显示人物
            ShowCharacters(scriptdatas[scriptIndex].Name, scriptdatas[scriptIndex].CharacterID);

            //更新人物位置
            SetcharacterPos(scriptdatas[scriptIndex].characterPos, scriptdatas[scriptIndex].ifTurnOver, scriptdatas[scriptIndex].CharacterID);
            if (!showCharacterOnly)
            {
                //更新对话框文本
                UpdateTalkLineText(scriptdatas[scriptIndex].TalkLine);
                //改变人物精力值和好感度
                ChangeEnergyValue(scriptdatas[scriptIndex].Energy);
                ChangeFavorValue(scriptdatas[scriptIndex].Favorability, scriptdatas[scriptIndex].Name);
            }

        }
        // 显示多项选择框
        public void ShowChoiceUI(int choicenum, string[] choicecontent)
        {
            UIManger.Instance.ShowChoiceUI(choicenum, choicecontent);
        }
        /// <summary>
        /// 向下获取剧本
        /// </summary>
        /// <param name="Num">获取数量</param>
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
        /// 设置剧本索引
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
                    Debug.Log("找到跳转的剧本索引");
                    break;
                }
            }
            HandData();
        }
    }
}

