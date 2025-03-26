using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Bomberman
{
    public class BombControl : MonoBehaviour
    {
        public GameObject EffectPre;
        void Start()
        {
            Invoke("Boom", 2f);
        }

        private void Boom()
        {
            GameObject effet = Instantiate(EffectPre, transform.position, transform.rotation);

            Destroy(effet, 2f);

            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemys)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < 3f)
                {
                    Destroy(enemy);
                }
            }

            Destroy(gameObject);
        }
    }
}

