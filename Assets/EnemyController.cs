using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float speed = 10.0f;
    public Transform[] patrolPoints;
    public bool drawPatrolGizmos = false;
    private int patrolIndex = 0;
    
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, patrolPoints[patrolIndex].position) < 0.5f)
        {
            //navMeshAgent.SetDestination(patrolPoints[patrolIndex++].position);
            patrolIndex++;

            if (patrolIndex >= patrolPoints.Length)
            {
                patrolIndex = 0;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[patrolIndex].position,
                speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (drawPatrolGizmos)
        {
            for (int i = 0; i < patrolPoints.Length - 1; i++)
            {
                Debug.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position, Color.green);
            }

            if (patrolPoints.Length > 1)
            {
                Debug.DrawLine(patrolPoints[0].position, patrolPoints[patrolPoints.Length - 1].position, Color.green);
            }
        }
    }
}
