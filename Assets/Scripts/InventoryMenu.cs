using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{

    public GameObject[] InvSlots;

    int current;

    bool Active = false;

    private PlayerControl Player;
    private InventoryManager PlayerInv;

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        PlayerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
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
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                Player.OpenMenu();
                Active = true;
                
            }
            else if (Active == true)
            {
                GetComponent<CanvasGroup>().alpha = 0;
                GetComponent<CanvasGroup>().interactable = false;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
                Player.CloseMenu();
                Active = false;
            }
        }
    }

    public void AddItem(GameObject item)
    {

        InvSlots[current].GetComponent<InvSlotMenu>().Populate(item, current);
        current++;

    }

    public void DropItem(int slot)
    {

        PlayerInv.DropWeapon(InvSlots[slot].GetComponent<InvSlotMenu>().WeaponRef);

        InvSlots[slot].GetComponent<InvSlotMenu>().Populate(null, current);



    }



}
