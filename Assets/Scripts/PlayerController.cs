using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Movement;

public class PlayerController : MonoBehaviour
{
    public float hp = 1000.0f;
    public float speed = 10.0f; 
    Vector3 goToPoint;
    NavMeshAgent navMeshAgent;

    private GameObject targetObject = null;
    
    void Start()
    {
        goToPoint = transform.position;
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            int mouseMask = ~LayerMask.GetMask("PlayerCharacter");

            if (Physics.Raycast(mouseRay, out rhInfo, 50.0f, mouseMask))
            {
                Debug.Log("Mouse ray hit:" + rhInfo.collider.gameObject.name + " at " + rhInfo.point);

                if (rhInfo.collider.gameObject.tag == "Enemy")
                {
                    targetObject = rhInfo.collider.gameObject;
                    targetObject.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if(targetObject != null)
                {
                    targetObject.transform.GetChild(1).gameObject.SetActive(false);
                    targetObject = null;
                }

                goToPoint = rhInfo.point;
                goToPoint.y = transform.position.y;

                navMeshAgent.SetDestination(goToPoint);
            }
            else
            {
                Debug.Log("Mouse ray hit nothing");
            }
        }

        //float distToPoint = Vector3.Distance(transform.position, goToPoint);
        //if (distToPoint > 0.1f)
        //{
        //    transform.LookAt(goToPoint);
        //    transform.position += transform.forward * Time.deltaTime * speed;
        //}
    }
}
