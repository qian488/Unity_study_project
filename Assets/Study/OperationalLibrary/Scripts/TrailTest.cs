using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Operation
{
    public class TrailTest : MonoBehaviour
    {
        private TrailRenderer trailRenderer;
        private Vector3 center;

        void Start()
        {
            trailRenderer = GetComponent<TrailRenderer>();
            center = transform.position - Vector3.right * 5;
            trailRenderer.startColor = Color.red;
            trailRenderer.endColor = Color.blue;
            trailRenderer.startWidth = 1f;
            trailRenderer.endWidth = 0.1f;
            trailRenderer.time = 0.5f;
        }

        
        void Update()
        {
            transform.RotateAround(center,Vector3.forward,Time.deltaTime * 360);
        }
    }
}

