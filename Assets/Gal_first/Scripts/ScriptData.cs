using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gal_first 
{
    //剧本数据
    public class ScriptData
    {
        public int LoadType;//载入资源类型，1.背景 2.角色 3.事件
        public string Name;//角色名称
        public string SpirteName;//图片资源路径
        public string TalkLine;//对话内容
        public int characterPos;//角色位置，1.左 2.中 3.右
        public bool ifTurnOver;//角色图片翻转
        public int SoundType;//音频类型，1.角色音 2.特效音 3.背景音
        public string SoundPath;//音频路径
        public int Favorability;//好感度（改变值，即Δ）
        public int Energy;//精力值（改变值，即Δ）
        public int CharacterID;//三人对话时，角色ID
        public int EventID;
        //处理事件的ID
        //1.显示选择项 2.跳转到指定剧本位置 3.显示隐藏遮罩 4.特殊事情 5.显示隐藏人物
        public int EventData;
        //事件数据
        //1.几个选项 2.具体要跳转到的标记位 3.0隐藏1显示 4.事件ID 5.0退场1进场
        public int ScriptID;//剧本标记位，用于跳转
        public int scriptIndex;//剧本索引
    }

}


