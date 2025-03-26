using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.GamePlay
{
    public class Tank : TankBase
    {
        public float fireOffset = 2f;
        private float nowTime = 0f;
        public float fireDistance = 5f;

        public GameObject bulletObj;
        public Transform[] shootPos;

        public Transform targetPos;
        public Transform[] randomPos;

        public Transform PlayerPos;

        public override void Fire()
        {
            for(int i = 0; i < shootPos.Length; i++)
            {
                GameObject obj = Instantiate(bulletObj, shootPos[i].position, shootPos[i].rotation);
                Bullet bullet = obj.GetComponent<Bullet>();
                bullet.SetFater(this);
            }
        }

        void Start()
        {
            RandomPos();
        }

        private void RandomPos()
        {
            if(randomPos.Length == 0)
            {
                Debug.Log("没有移动目标");
                return;
            }
            targetPos = randomPos[UnityEngine.Random.Range(0, randomPos.Length)];
        }

        void Update()
        {
            if(moveSpeed!=0)
            {
                this.transform.LookAt(targetPos);
                this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

                if (Vector3.Distance(this.transform.position, targetPos.position) < 0.1f)
                {
                    RandomPos();
                }
            }
            
            if(Vector3.Distance(this.transform.position, PlayerPos.position) < 30f)
            {
                head.LookAt(PlayerPos);
                nowTime += Time.deltaTime;
                if (nowTime > fireOffset)
                {
                    Fire();
                    nowTime = 0f;
                }
            }
            
        }

        public override void Dead()
        {
            base.Dead();
            GamePanel.Instance.AddScore(this.maxHP * 10);
        }


    }
}

