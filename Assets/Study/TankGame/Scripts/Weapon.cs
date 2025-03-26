using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.GamePlay
{
    public class Weapon : MonoBehaviour
    {
        public GameObject bullet;

        public Transform[] shootPos;

        public TankBase father;

        public void Fire()
        {
            for (int i = 0; i < shootPos.Length; i++)
            {
                GameObject obj = Instantiate(bullet, shootPos[i].position, shootPos[i].rotation * Quaternion.Euler(0, 0, 30));
                Bullet bulletobj = obj.GetComponent<Bullet>();
                bulletobj.SetFater(father);
            }
        }

        public void SetFater(TankBase tank)
        {
            father = tank;
        }

        public void ChangeBullet(GameObject obj)
        {
            bullet = obj;
        }
    }
}

