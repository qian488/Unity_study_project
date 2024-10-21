using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bomberman
{
    public class EnemyControl : MonoBehaviour
    {
        private GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < 10f)
            {
                transform.LookAt(player.transform);
                transform.Translate(Vector3.forward * 5f * Time.deltaTime);
            }

            if (distance < 2)
            {
                Debug.Log("Íæ¼ÒËÀÍö£¬ÓÎÏ·½áÊø");
                Time.timeScale = 0;
            }
        }
    }
}

