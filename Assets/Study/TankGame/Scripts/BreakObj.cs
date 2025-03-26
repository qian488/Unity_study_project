using System.Collections;
using System.Collections.Generic;
using TankGame.Data;
using UnityEngine;

namespace TankGame.GamePlay
{
    public class BreakObj : MonoBehaviour
    {
        public GameObject BreakEffect;
        public float durableValue = 50f;
        public float def = 10f;

        private void OnTriggerEnter(Collider other)
        {
            Bullet player = other.GetComponent<Bullet>();
            if(player == null)
            {
                Debug.Log("未获得玩家对象");
                return;
            } 
            ChangedDurable(player);
            if(durableValue <= 0)
            {
                GameObject effobj = Instantiate(BreakEffect,this.transform.position,this.transform.rotation);
                AudioSource audioSource = effobj.GetComponent<AudioSource>();
                audioSource.volume = GameDataManager.Instance.musicData.soundVolume;
                Destroy(this.gameObject);
            }
        }

        public void ChangedDurable(Bullet bullet)
        {
            TankBase player = bullet.father;
            float dmg = (player.atk - this.def) * 0.1f;
            durableValue -=  (dmg > 0) ? dmg : 0;
        }
    }
}

