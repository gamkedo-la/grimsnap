using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCollider : MonoBehaviour
{
    public GameObject rep;

    Vector3 temp = new Vector3(0,1000,0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (rep.GetComponent<InventoryObject>() != null)
        {
            temp.x = rep.GetComponent<RectTransform>().position.x;
            temp.z = rep.GetComponent<RectTransform>().position.y;

            transform.position = temp;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(rep.GetComponent<InventoryObject>() != null)
        {
            rep.GetComponent<InventoryObject>().AddOverlap(collision.gameObject.GetComponent<InventoryCollider>().rep);

        }

    }

    private void OnCollisionExit(Collision collision)
    {

        if (rep.GetComponent<InventoryObject>() != null)
        {

            rep.GetComponent<InventoryObject>().RemoveOverlap(collision.gameObject.GetComponent<InventoryCollider>().rep);
        }

    }
}
