using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Operation
{
    public class LineControl : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private int count = 0;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.green;
            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 0.5f;
        }

        void Update()
        {
            if(Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    lineRenderer.positionCount = ++count;
                    lineRenderer.SetPosition(count - 1, hit.point + Vector3.back * 2);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                lineRenderer.positionCount = 0;
                count = 0;
            }
        }
    }
}

