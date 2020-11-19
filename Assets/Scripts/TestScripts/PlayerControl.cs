using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;
using UnityEditor;
using UnityEngine.AI;
using MyMath;

//Processes data and issues commands, nothing else
public class PlayerControl : MonoBehaviour
{
    //Component Data
    PlayerInput playerInput;
    Move move;
    Animator animator;
    Attack attack;

    // Need to pull speed from movementData script, meleeRange and damage from equipped melee
    public float speed = 50;
    public float meleeRange = 2;
    public float damage = 5;

    //Data from ray cast
    RaycastHit raycastHit;
    Health target;

    //Other entities such as enemy and misc need access to MoveToTerrain and FollowAndAttackTarget... move to generic class and change to return bools?

    void Start()
    {
        move = GetComponent<Move>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
        attack = GetComponent<Attack>();
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
        if (!target.isDead && !animator.GetBool("Attack1"))
        {
            if (!VectorMath.IsWithinDistance(meleeRange, transform.position, target.transform.position))
            {
                animator.SetBool("isWalking", true);
                move.MoveToTarget(speed, target.transform.position);
            }
            else
            {
                animator.SetBool("isWalking", false);
                animator.SetTrigger("Attack1");
                move.StopMoving();
                attack.AttackTarget(target, damage);
            }
        }
    }

    private void MoveToTerrain()
    {
        float number = .1f; // Move this elsewhere
        if (VectorMath.IsAroundAtPoint(transform.position, raycastHit.point, number))
        {
            animator.SetBool("isWalking", false);
            return;
        }
        else
        {
            animator.SetBool("isWalking", true);
            move.MoveToTarget(speed, raycastHit.point);
        }
    }
}
