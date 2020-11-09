using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;
using UnityEditor;

public class PlayerControlTest : MonoBehaviour
{
    PlayerInput playerInput;
    Move move;
    public float speed = 50;

    void Start()
    {
        move = GetComponent<Move>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        ClickToMove();
    }

    void ClickToMove() 
    {
        if (playerInput.leftClick) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 100)) 
            {
                if (raycastHit.transform.tag == "Ground")
                {
                    move.MoveToTarget(speed, raycastHit.point);
                }

                if (raycastHit.transform.tag == "Enemy")
                {
                    move.MoveToTarget(speed, raycastHit.point); // Change this to follow target later
                }
            }
        }
    }
}
