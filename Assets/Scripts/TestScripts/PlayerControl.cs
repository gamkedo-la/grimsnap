﻿using System;
using System.Collections;
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
    TargetHealth targetHealth;
    public bool canDealDamageThisFrame = false;

    public Transform debugSpot;

    // Need to pull speed from movementData script, meleeRange and damage from equipped melee
    public float speed = 50;
    public float meleeRange = 2;
    public float damage = 5;
    public float armor = 0;

    //Data from ray cast
    RaycastHit click;
    Health target;

    public GameObject pickUpTarget;

    private bool MenuOpen = false;

    private bool isRunning = false;

    [SerializeField]
    private GameObject warpPoint1, warpPoint2, warpPoint3, warpPoint4;

    //Other entities such as enemy and misc need access to MoveToTerrain and FollowAndAttackTarget... move to generic class and change to return bools?
    [SerializeField] GrimSnapAudio.AudioPlayer playerAudio;

    void Start()
    {
        move = GetComponent<Move>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
        attack = GetComponent<Attack>();
        Inv = GetComponent<InventoryManager>();
        targetHealth = GetComponent<TargetHealth>();
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
            if (!isRunning && playerInput.running)
            {
                Debug.Log("player starts running");
            }
            isRunning |= playerInput.running;

            //if (Physics.Raycast(ray, out raycastHit, 100))
            //{

            //       //target = raycastHit.transform.GetComponent<Health>();


            //}
        }
        if (Input.GetMouseButton(0))
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
        if (click.transform == null) return;
        if (click.transform.tag == "Enemy")
        {
            target = click.transform.GetComponent<Health>();
            FollowTarget();
            //Debug.Log("following " + target.name);
            targetHealth.ShowTargetHealth(target);

            return;
        }
        if (click.transform.tag == "Equipment")
        {
            //Debug.Log("hit " + click.transform.gameObject.name);
            pickUpTarget = click.transform.gameObject;

        }
        if (click.transform.tag != "Enemy")
        {
            targetHealth.HideTargetHealth();
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
                StopRunning();
                this.transform.LookAt(target.transform.position);
                attack.AttackTarget(target, damage);
            }
        }
    }

    private float GetCurrentSpeed()
    {
        float currentSpeed = isRunning || playerInput.running ? speed * 2f : speed;
        return currentSpeed;
    }

    private void MoveToTerrain()
    {
        float number = .1f; // Move this elsewhere
        if (VectorMath.IsAroundAtPoint(transform.position, click.point, number))
        {
            animator.SetBool("isWalking", false);
            StopRunning();
            return;
        }
        else
        {
            animator.SetBool("isWalking", true);
            Vector3 terrainPoint;
            if (click.transform.tag.Equals("Ground"))
            {
                terrainPoint = click.point;
            }
            else
            {
                Collider clickCollider = click.collider;
                Transform transformParent = clickCollider.transform;

                while (transformParent.transform.parent != null)
                {
                    Collider componentInParent = transformParent.GetComponentInParent<Collider>();
                    if (componentInParent != null)
                    {
                        clickCollider = componentInParent;
                    }
                    transformParent = transformParent.transform.parent;
                    Debug.Log(clickCollider.name);
                }

                Bounds clickColliderBounds = clickCollider.bounds;
                terrainPoint = clickColliderBounds.ClosestPoint(transform.position);

                // debugSpot.position = terrainPoint + Vector3.up*2.0f;
                //keep on ground
                terrainPoint.y = 0.2f;
            }

            // move.MoveToTarget(GetCurrentSpeed(), raycastHit.point);
            move.MoveToTarget(GetCurrentSpeed(), terrainPoint);
        }
    }

    private void StopRunning()
    {
        if (isRunning)
        {
            Debug.Log("player stops running");
        }
        isRunning = false;
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

            if (playerAudio != null)
            {
                playerAudio.ItemPickUpAudio();
            }

            pickUpTarget = null;
        }
    }

    public void UpdateGearScore(float r, float d, float a)
    {
        meleeRange = r;
        damage = d;
        armor = a;

        GetComponent<Health>().armorModifier = 1 - ((0.03f * armor) / (1 + (0.04f * Mathf.Abs(armor))));

    }

    public void ClearTarget()
    {
        click = new RaycastHit();
        move.StopMoving();
    }

    public void ToggleCanDealDamageThisFrame()
    {
        canDealDamageThisFrame = true;
    }
}
