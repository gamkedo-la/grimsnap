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
    [Space]
    public float restAfterMinDelay = 1.6f;
    public float restAfterMaxDelay = 2.4f;
    public float restMinTime = 0.4f;
    public float restMaxTime = 1.2f;
    [Space]
    public Animator animator;
    
    private int patrolIndex = 0;

    private NavMeshAgent navMeshAgent;
    
    private float restTimer = 0.0f;
    private bool rest = true;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        restTimer = UnityEngine.Random.Range(restMinTime, restMaxTime);
    }
    
    void Update()
    {
        if (!rest)
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
                animator.SetBool("isWalking", true);

                Vector3 targetDirection = patrolPoints[patrolIndex].position - transform.position;
                float singleStep = speed * 4.0f * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (restTimer <= 0.0f)
        {
            rest = !rest;
            if (rest)
                restTimer = UnityEngine.Random.Range(restMinTime, restMaxTime);
            else
                restTimer = UnityEngine.Random.Range(restAfterMinDelay, restAfterMaxDelay);
        }
        else
        {
            restTimer -= Time.deltaTime;
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
