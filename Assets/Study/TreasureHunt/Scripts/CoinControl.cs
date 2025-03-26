using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
namespace TreasureHunt
{
    public class CoinControl : MonoBehaviour
    {
        public GameObject EffectPre;
        public static int Count = 0;

        private Transform player;
        public AudioSource audioSource;

        void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
            Debug.Log(player);
        }

        void Update()
        {
            float dis = Vector3.Distance(transform.position, player.position);

            if (dis <= 1.5f)
            {
                Count++;
                Debug.Log("Count" + Count);
                // audioSource.PlayOneShot(audioSource.clip); // ²¥·Å½ð±ÒÒôÐ§

                if (Count >= 5)
                {
                    GameObject go = GameObject.FindWithTag("EffectTrriger");
                    EffectControl effectControl = go.AddComponent<EffectControl>();
                    effectControl.EffectPre = EffectPre;
                    effectControl.player = player;
                }

                Destroy(this);
                Destroy(gameObject, 0.3f);
            }

            /*
            cnt++;

            if (cnt <= 5)
            {
                GameObject effect = Instantiate(EffectPre, player.position, player.rotation);
                Destroy(effect, 6f);
                cnt = 10;
            }
            */
        }
    }

}
