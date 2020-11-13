using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;
using UnityEditor;
using UnityEngine.AI;

//Processes data and issues commands, nothing else
public class PlayerControl : MonoBehaviour
{
    //Component Data
    PlayerInput playerInput;
    Move move;
    Animator animator;

    // Need to pull speed from movementData script, distance to stop from equipped melee
    public float speed = 50;
    float meleeRange = 2;

    //Data from ray cast
    RaycastHit raycastHit;
    Health target;

    //Other entities such as enemy and misc need access to MoveToTerrain and FollowAndAttackTarget... move to generic class and change to return bools?

    void Start()
    {
        move = GetComponent<Move>();
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
        if (raycastHit.transform.tag != "Ground" && raycastHit.transform.tag != "Obstacle")
        {
            FollowTarget();
            return;
        }
        MoveToTerrain();
    }

    private void FollowTarget()
    {
        if (Conditionals.IsTargetOutOfRangeAndAlive(meleeRange, target, transform.position))
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
        float number = .1f; // Move this elsewhere
        if (!Conditionals.IsAroundAtPoint(transform.position, raycastHit.point, number))
        {
            Debug.Log("We're here");
            animator.SetBool("isWalking", false);
            return;
        }
        else
        {
            Debug.Log("Now we're here");
            animator.SetBool("isWalking", true);
            move.MoveToTarget(speed, raycastHit.point);
        }
    }
}
