using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Movement 
{
    /// <summary>
    /// Move the character to a given point, with no conditionals
    /// </summary>
    public class Move : MonoBehaviour
    {

        NavMeshAgent navMeshAgent;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            // speed property is max speed, so maybe use higher acceleration for more control?
            navMeshAgent.acceleration *= 3;
        }
        private void Update()
        {
            
        }

        public void MoveToTarget(float speed, Vector3 pos)
        {
            navMeshAgent.speed = speed;
            navMeshAgent.SetDestination(pos);
        }

        public void StopMoving()
        {
            navMeshAgent.SetDestination(transform.position);
            Debug.Log("stopping");
        }

        public NavMeshAgent GetNavMeshAgent()
        {
            return navMeshAgent;
        }

        public void warpToPosition(GameObject warpPosition){
            this.transform.position = warpPosition.transform.position;
            GetComponent<PlayerControl>().ClearTarget();
        }
    }

}

