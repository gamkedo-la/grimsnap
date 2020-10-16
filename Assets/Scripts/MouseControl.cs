using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseControl : MonoBehaviour
{
    public float speed = 10.0f;
    Vector3 goToPoint;
    NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        goToPoint = transform.position;
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse clicked");

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            int mouseMask = ~LayerMask.GetMask("PlayerCharacter");

            if (Physics.Raycast(mouseRay, out rhInfo, 50.0f, mouseMask))
            {
                Debug.Log("Mouse ray hit:" + rhInfo.collider.gameObject.name + " at " + rhInfo.point);

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
