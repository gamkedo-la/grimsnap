using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;
using UnityEditor;

public class PlayerControlTest : MonoBehaviour
{
    PlayerInput playerInput;
    MoveTest move;
    TagsTest tagsSaved;
    public float speed = 50;

    void Start()
    {
        move = GetComponent<MoveTest>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        CheckPrimaryButton();
    }

    void CheckPrimaryButton() 
    {
        if (playerInput.leftClick) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 100))
            {
                CheckForAction(raycastHit);
            }
        }
    }

    private void CheckForAction(RaycastHit raycastHit)
    {
        tagsSaved = raycastHit.transform.GetComponent<TagsTest>();
        if (tagsSaved.tags.Contains(Tags.Ground)) 
        {
            move.SetPosition(speed, raycastHit.point);
        }
        if (tagsSaved.tags.Contains(Tags.CanTarget))
        {
            move.SetTarget(speed, 2, raycastHit.transform); // Change the hardcoded distance to something Serialized
        }
    }
}
