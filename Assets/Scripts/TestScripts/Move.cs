using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Movement 
{
    public class Move : MonoBehaviour
    {

        NavMeshAgent navMeshAgent;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void MoveToTarget(float speed, Vector3 target)
        {
            navMeshAgent.speed = speed;
            navMeshAgent.SetDestination(target);
        }

        public void FollowTarget() // follows the target until it reaches it... for enemies that move
        { 
            // Needs to evaluate whether it's in range or not... 
            // make this an enumator that stays active when called and then deactive if clicked on something else? 
            // Just needs to either stay active until the target is in distance or something else is clicked
            // Needs more thought, save for later
        }
    }

}

