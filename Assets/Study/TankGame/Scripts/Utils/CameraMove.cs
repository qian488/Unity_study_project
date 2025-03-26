using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.GamePlay 
{
    public class CameraMove : MonoBehaviour
    {
        public Transform targetPlayer;

        public float height = 10;

        private Vector3 pos;
        void LateUpdate()
        {
            if (targetPlayer == null) return;

            pos.x = targetPlayer.position.x;
            pos.z = targetPlayer.position.z;
            pos.y = height;
            this.transform.position = pos;
        }
    }

}

