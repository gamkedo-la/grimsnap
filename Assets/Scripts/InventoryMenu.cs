using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{

    public GameObject[] InvSlots;

    int current;

    bool Active = false;

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (Active == false)
            {
                GetComponent<CanvasGroup>().alpha = 1;
                GetComponent<CanvasGroup>().interactable = true;
                Active = true;
                
            }
            else if (Active == true)
            {
                GetComponent<CanvasGroup>().alpha = 0;
                GetComponent<CanvasGroup>().interactable = false;
                Active = false;
            }
        }
    }

    public void AddItem(GameObject item)
    {

        InvSlots[current].GetComponent<InvSlotMenu>().Populate(item);

    }

    public void DropItem()
    {




    }



}
