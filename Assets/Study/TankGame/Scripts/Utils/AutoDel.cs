using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.GamePlay
{
    public class AutoDel : MonoBehaviour
    {
        public float time = 2f;
        void Start()
        {
            Destroy(this.gameObject,time);
        }

    }

}
