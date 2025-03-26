using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunt
{
    public class ChestControl : MonoBehaviour
    {
        public GameObject CoinPre;
        private Transform player;

        void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
        }


        void Update()
        {
            float dis = Vector3.Distance(transform.position, player.position);

            if (dis < 2 && Input.GetMouseButtonDown(0))
            {
                if (Random.Range(0, 10) > 6)
                {
                    Instantiate(CoinPre, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
    }
}

