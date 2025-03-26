using System.Collections;
using System.Collections.Generic;
using TankGame.Data;
using UnityEngine;

namespace TankGame.GamePlay
{
    public abstract class TankBase : MonoBehaviour
    {
        public int atk;
        public int def;
        public int maxHP = 100;
        public int nowHP = 60;
        public float moveSpeed = 5;
        public float rotateSpeed = 30;
        public float headRotateSpeed = 50;

        public Transform head;

        public GameObject deadEff;

        public abstract void Fire();

        public virtual void Wound(TankBase other)
        {
            int dmg = other.atk - this.def;
            if (dmg < 0) return;
            this.nowHP -= dmg;
            if (nowHP <= 0) Dead();
        }

        public virtual void Dead()
        {
            Destroy(this.gameObject);
            if (this.deadEff != null )
            {
                GameObject obj = Instantiate(deadEff, this.transform.position , this.transform.rotation);
                AudioSource audioSource = obj.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.volume = GameDataManager.Instance.musicData.soundVolume;
                    audioSource.Play();
                }
            }
        }
    }
}

