using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.GamePlay
{
    public class EndPoint : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Time.timeScale = 0;
                WinPanel.Instance.Show();
            }
        }
    }
}

