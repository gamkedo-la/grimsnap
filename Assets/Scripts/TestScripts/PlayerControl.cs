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
    InventoryManager Inv;

    // Need to pull speed from movementData script, meleeRange and damage from equipped melee
    public float speed = 50;
    public float meleeRange = 2;
    public float damage = 5;

    //Data from ray cast
    RaycastHit raycastHit;
    RaycastHit click;
    Health target;

    public GameObject pickUpTarget;

    private bool MenuOpen = false;

    //Other entities such as enemy and misc need access to MoveToTerrain and FollowAndAttackTarget... move to generic class and change to return bools?

    void Start()
    {
        move = GetComponent<Move>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
        attack = GetComponent<Attack>();
        Inv = GetComponent<InventoryManager>();
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
        if (playerInput.leftClick && MenuOpen == false) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out raycastHit, 100);
            //if (Physics.Raycast(ray, out raycastHit, 100))
            //{

            //       //target = raycastHit.transform.GetComponent<Health>();
                    
                
            //}
        }
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out click, 100);

        }


    }

    private void CheckForAction()
    {
        if (raycastHit.transform == null) return;
        if (click.transform.tag == "Enemy")
        {
            target = click.transform.GetComponent<Health>();
            FollowTarget();
            //Debug.Log("following " + target.name);
            return;
        }
        if (click.transform.tag == "Equipment")
        {
            //Debug.Log("hit equipment");
            pickUpTarget = click.transform.gameObject;

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

    public void OpenMenu()
    {

        MenuOpen = true;

    }

    public void CloseMenu()
    {

        MenuOpen = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject == pickUpTarget)
        {
            Debug.Log("picking up " + collision.gameObject.name);
            collision.transform.position += (Vector3.down * 20);
            Inv.CollectWeapon(collision.gameObject);
            pickUpTarget = null;
        }
    }
}
