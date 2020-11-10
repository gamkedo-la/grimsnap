using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;
using UnityEditor;
using UnityEngine.AI;

public class PlayerControlTest : MonoBehaviour
{
    PlayerInput playerInput;
    MoveTest move;
    TagsTest tagsSaved;
    public float speed = 50;
    Transform target = null;
    float distanceToStop = 2;
    RaycastHit raycastHit;

    void Start()
    {
        move = GetComponent<MoveTest>();
        playerInput = GetComponent<PlayerInput>();
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
                if (raycastHit.transform != target)
                {
                    target = raycastHit.transform;
                    tagsSaved = raycastHit.transform.GetComponent<TagsTest>();
                }
            }
        }
    }

    private void CheckForAction()
    {
        if (tagsSaved.tags.Contains(Tags.Ground)) 
        {
            move.MoveToTarget(speed, raycastHit.point);
        }
        if (tagsSaved.tags.Contains(Tags.Enemy) && distanceToStop < VectorMath.DistanceAbs(transform.position, raycastHit.transform.position))
        {
            move.MoveToTarget(speed, target.position);
        }
        if (tagsSaved.tags.Contains(Tags.Enemy) && distanceToStop > VectorMath.DistanceAbs(transform.position, raycastHit.transform.position))
        {
            move.StopMoving();
        }
    }
}
