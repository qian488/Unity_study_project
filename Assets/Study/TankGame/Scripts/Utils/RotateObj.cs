using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.GamePlay
{
    public class RotateObj : MonoBehaviour
    {
        public float rotateSpeed = 10f;
        void Update()
        {
            this.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}

