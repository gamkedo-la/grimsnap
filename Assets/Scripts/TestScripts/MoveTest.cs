using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Movement 
{
    public class MoveTest : MonoBehaviour
    {

        NavMeshAgent navMeshAgent;

        float distanceToStop;
        Transform target;
        Vector3 pos =  Vector3.zero;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // check if target != null then keep moviong to it, always need a range check then
        private void Update()
        {
            MoveToTarget();
            MoveToPos();
        }

        private void MoveToTarget()
        {
            if (target != null)
            {
                if (distanceToStop < Mathf.Abs(Vector3.Distance(target.position, transform.position)))
                {
                    navMeshAgent.SetDestination(target.position);
                }
                else
                {
                    navMeshAgent.SetDestination(transform.position);
                }
            }
        }

        private void MoveToPos() 
        {
            if (pos != Vector3.zero) 
            {
                navMeshAgent.SetDestination(pos);
                pos = Vector3.zero;
            }
        }

        public void SetTarget(float speed, float distanceToStop, Transform target) 
        {
            navMeshAgent.speed = speed;
            this.distanceToStop = distanceToStop;
            this.target = target;
        }

        public void SetPosition(float speed, Vector3 pos)
        {
            target = null;
            this.pos = pos;
        }
    }

}

