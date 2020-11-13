using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;
using UnityEditor;
using UnityEngine.AI;

//Processes data and issues commands, nothing else
public class PlayerControlTest : MonoBehaviour
{
    // This script needs to get distanceToStop and speed from other data sources
    PlayerInput playerInput;
    MoveTest move;
    TagsTest tagsSaved;
    public float speed = 50;
    //Transform target = null;
    float distanceToStop = 2;
    RaycastHit raycastHit;
    Animator animator;
    Vector3 destination = new Vector3(0, 0, 0);
    Health target;

    void Start()
    {
        move = GetComponent<MoveTest>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        CheckPrimaryButtonForTarget();
        CheckForAction();
    }

    /// <summary>
    /// Gives raycastHit data on left click and updates target with a new transform if a new target is aquired
    /// </summary>
    void CheckPrimaryButtonForTarget() 
    {
        if (playerInput.leftClick) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100))
            {
                target = raycastHit.transform.GetComponent<Health>();
            }
        }
    }

    private void CheckForAction()
    {
        if (raycastHit.transform == null) return;
        MoveToTerrain();
        FollowAndAttackTarget(distanceToStop);
    }

    private void FollowAndAttackTarget(float meleeRange)
    {
        if (target.isDead) return;

        if (!VectorMath.WithinDistance(meleeRange, transform.position, raycastHit.transform.position))
        {
            animator.SetBool("isWalking", true);
            move.MoveToTarget(speed, target.transform.position);
        }
        else
        {
            animator.SetBool("isWalking", false);
            move.StopMoving();
        }
    }

    private void MoveToTerrain()
    {
        float number = .1f;
        if (transform.position.x <= raycastHit.point.x + number && transform.position.z <= raycastHit.point.z + number &&
            transform.position.x >= raycastHit.point.x - number && transform.position.z >= raycastHit.point.z - number)
        {
            animator.SetBool("isWalking", false);
            return;
        }
        if (raycastHit.transform.tag == "Ground")
        {
            animator.SetBool("isWalking", true);
            move.MoveToTarget(speed, raycastHit.point);
        }
    }
}
