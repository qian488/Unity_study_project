using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bomberman
{
    public class CameraControl : MonoBehaviour
    {
        private Vector3 vector;
        private Transform player;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
            vector = player.transform.position - transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = player.transform.position - vector;
        }
    }

}
