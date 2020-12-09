using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    public GameObject RealObject;
    public List<InventoryGridNode> Location = new List<InventoryGridNode>();

    public InventoryMenu menu;

    public bool selected = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (selected == true)
        {

            transform.position = Input.mousePosition;
        }
        
    }

    public void ItemClicked()
    {
        Debug.Log("clicked " + RealObject.name + " in inventory");
        if(selected == false && menu.selected == null)
        {

            selected = true;
            menu.selected = this;
        }
        else if (selected == true  && menu.selected == this)
        {

            selected = false;
            menu.selected = null;
        }

    }
}
