using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Operation
{
    public class NavigationTest : MonoBehaviour
    {
        private NavMeshAgent agent;
        public Transform target;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }


        void Update()
        {
            agent.SetDestination(target.position);
        }
    }
}

