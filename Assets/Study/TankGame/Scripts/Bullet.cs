using System.Collections;
using System.Collections.Generic;
using TankGame.Data;
using UnityEngine;

namespace TankGame.GamePlay
{
    public class Bullet : MonoBehaviour
    {
        public float moveSpeed = 50f;

        public TankBase father;

        public GameObject eff;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Cube") || 
               other.CompareTag("Break") ||
               (other.CompareTag("Player") && father.CompareTag("Tank"))||
               (other.CompareTag("Tank") && father.CompareTag("Player")))
            {
                TankBase tank = other.GetComponent<TankBase>();
                if(tank != null)
                {
                    tank.Wound(father);
                } 

                if (eff != null)
                {
                    GameObject obj = Instantiate(eff,this.transform.position,this.transform.rotation);
                    AudioSource audioSource = obj.GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        audioSource.volume = GameDataManager.Instance.musicData.soundVolume;
                        audioSource.Play();
                    }
                }
                Destroy(this.gameObject);
            }
        }

        public void SetFater(TankBase tank)
        {
            father = tank;
        }
    }
}

