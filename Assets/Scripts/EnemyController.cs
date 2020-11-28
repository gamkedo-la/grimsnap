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
    private int PPPrevious;

    private NavMeshAgent navMeshAgent;
    
    private float restTimer = 0.0f;
    private bool rest = true;

    //public Collider[] SphereOverlapArray;
    public GameObject Player;
    public int VisualRange;
    public int WanderRadius;
    public int WR;

    public float AttackTimer;
    public float AttackLoop;
    private float AttackReset;

    public int AttackDamage;

    public int AttackRange;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        restTimer = UnityEngine.Random.Range(restMinTime, restMaxTime);
        Player = GameObject.FindGameObjectWithTag("Player");
        WR = WanderRadius;
               
        AttackReset = AttackTimer;
        

    }
    
    void Update()
    {
        //OverlapSphere returns an array of colliders but by using the layer mask it 
        //should only ever return one object, the player character

        //SphereOverlapArray = Physics.OverlapSphere(transform.position, VisualRange);
        //foreach(var hitCollider in SphereOverlapArray)
        //{
        //    if(hitCollider.gameObject.tag == "Player")
        //    {

        //        target = hitCollider.gameObject;
        //    }

        //}

        if (Vector3.Distance(transform.position, Player.transform.position) <= AttackRange)
        {

            Vector3 targetDirection = Player.transform.position - transform.position;
            float singleStep = speed * 4.0f * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);


            animator.SetBool("isAttacking", true);
            AttackTimer -= Time.deltaTime;
            if (AttackTimer <= 0)
            {
                GetComponent<Attack>().AttackTarget(Player.GetComponent<Health>(), AttackDamage);
                AttackTimer = AttackLoop;

            }


        }
        else
        {
            animator.SetBool("isAttacking", false);
            AttackTimer = AttackReset;



             


            if (Vector3.Distance(transform.position, Player.transform.position) < VisualRange 
                && Vector3.Distance(transform.position,
                ClosestPointOnLine(patrolPoints[PPPrevious].position, patrolPoints[patrolIndex].position, 
                Player.transform.position))
                < WanderRadius)
            {

                animator.SetBool("isWalking", true);
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position,
                    speed * Time.deltaTime);


                Vector3 targetDirection = Player.transform.position - transform.position;
                float singleStep = speed * 4.0f * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);

                if (Vector3.Distance(transform.position,
                    ClosestPointOnLine(patrolPoints[PPPrevious].position, patrolPoints[patrolIndex].position, Player.transform.position))
                    > WanderRadius)
                {
                    WanderRadius = 0;

                }
                return;
            }


            if (!rest)
            {

                if (Vector3.Distance(transform.position, patrolPoints[patrolIndex].position) < 0.5f)
                {
                    //navMeshAgent.SetDestination(patrolPoints[patrolIndex++].position);
                    patrolIndex++;
                    WanderRadius = WR;

                    if (patrolIndex >= patrolPoints.Length)
                    {
                        patrolIndex = 0;
                    }

                    PPPrevious = patrolIndex - 1;
                    if (PPPrevious < 0)
                    {
                        PPPrevious = patrolPoints.Length - 1;
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

    private Vector3 ClosestPointOnLine(Vector3 LineOrigin, Vector3 LineEnd, Vector3 Point)
    {

        Vector3 heading = (LineEnd - LineOrigin);
        float magnitudeMax = heading.magnitude;
        heading.Normalize();

        Vector3 lhs = Point - LineOrigin;
        float dotP = Vector3.Dot(lhs, heading);
        dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
        return (LineOrigin + heading * dotP);

    }

}
