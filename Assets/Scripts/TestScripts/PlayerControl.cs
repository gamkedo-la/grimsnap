﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;
using UnityEditor;
using UnityEngine.AI;
using MyMath;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI targetNameText;
    public Image targetHealthImage;
    public Image targetHealthImageFill;

    public GameObject pickUpTarget;

    private bool MenuOpen = false;

    [SerializeField]
    private GameObject warpPoint1, warpPoint2, warpPoint3, warpPoint4;

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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            move.warpToPosition(warpPoint1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            move.warpToPosition(warpPoint2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            move.warpToPosition(warpPoint3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            move.warpToPosition(warpPoint4);
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
            ShowTargetHealth();
            return;
        }
        if (click.transform.tag == "Equipment")
        {
            //Debug.Log("hit equipment");
            pickUpTarget = click.transform.gameObject;

        }
        if (click.transform.tag != "Enemy")
        {
            HideTargetHealth();
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
                move.MoveToTarget(GetCurrentSpeed(), target.transform.position);
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

    private float GetCurrentSpeed()
    {
        return playerInput.running?speed*2f:speed;
    }

    private void ShowTargetHealth()
    {
        if (target != null)
        {
            if (target.health <= 0) 
            { 
                HideTargetHealth(); 
            }
            else
            {
                targetNameText.enabled = true;
                targetHealthImage.enabled = true;
                targetHealthImageFill.enabled = true;
                targetNameText.text = target.name;
                targetHealthImageFill.fillAmount = target.health / target.maxHealth;
            }
        }
    }

    private void HideTargetHealth()
    {
        targetNameText.enabled = false;
        targetHealthImage.enabled = false;
        targetHealthImageFill.enabled = false;
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
            move.MoveToTarget(GetCurrentSpeed(), raycastHit.point);
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
        Debug.Log("hit " + collision.gameObject.name);
        if (collision.gameObject == pickUpTarget)
        {
            Debug.Log("picking up " + collision.gameObject.name);
            if (Inv.CollectWeapon(collision.gameObject))
            {

                collision.transform.position += (Vector3.down * 20);
            }

            pickUpTarget = null;
        }
    }

    public void UpdateGearScore(float r, float d, float a)
    {
        meleeRange = r;
        damage = d;

        //Some kind of defense modifier can be added if we want
        //armor = a;


    }
}
