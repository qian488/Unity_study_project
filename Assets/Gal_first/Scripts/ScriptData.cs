using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gal_first 
{
    //�籾����
    public class ScriptData
    {
        public int LoadType;//������Դ���ͣ�1.���� 2.��ɫ 3.�¼�
        public string Name;//��ɫ����
        public string SpirteName;//ͼƬ��Դ·��
        public string TalkLine;//�Ի�����
        public int characterPos;//��ɫλ�ã�1.�� 2.�� 3.��
        public bool ifTurnOver;//��ɫͼƬ��ת
        public int SoundType;//��Ƶ���ͣ�1.��ɫ�� 2.��Ч�� 3.������
        public string SoundPath;//��Ƶ·��
        public int Favorability;//�øжȣ��ı�ֵ��������
        public int Energy;//����ֵ���ı�ֵ��������
        public int CharacterID;//���˶Ի�ʱ����ɫID
        public int EventID;
        //�����¼���ID
        //1.��ʾѡ���� 2.��ת��ָ���籾λ�� 3.��ʾ�������� 4.�������� 5.��ʾ��������
        public int EventData;
        //�¼�����
        //1.����ѡ�� 2.����Ҫ��ת���ı��λ 3.0����1��ʾ 4.�¼�ID 5.0�˳�1����
        public int ScriptID;//�籾���λ��������ת
        public int scriptIndex;//�籾����
    }

}


