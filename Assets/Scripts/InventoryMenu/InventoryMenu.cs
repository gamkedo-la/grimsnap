using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{

    public List<GameObject> InvSlots = new List<GameObject>();

    public List<GameObject> OpenInvSlots = new List<GameObject>();

    public List<GameObject> PL = new List<GameObject>();

    public Vector2 temp;

    public GameObject Grid;

    public GameObject InvSpritePrefab;

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

        foreach (Transform g in Grid.transform)
        {
            InvSlots.Add(g.gameObject);
            OpenInvSlots.Add(g.gameObject);

        }
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
        temp = item.GetComponent<EquipableWeapon>().GetInvDim();

    }

    public void DropItem(int slot)
    {

        PlayerInv.DropWeapon(InvSlots[slot].GetComponent<InvSlotMenu>().WeaponRef);

        InvSlots[slot].GetComponent<InvSlotMenu>().Populate(null, current);

        for (int c = current; c < PlayerInv.GetCountOfWeapons(); c++)
        {
            InvSlots[c].GetComponent<InvSlotMenu>().Populate(InvSlots[c + 1].GetComponent<InvSlotMenu>().WeaponRef, current);
        }



        current = PlayerInv.GetCountOfWeapons();

        InvSlots[current].GetComponent<InvSlotMenu>().Populate(null, current);

    }

    private void CheckHoriz()
    {

        if (temp.x == 1)
        {
            foreach (GameObject n in OpenInvSlots)
            {
                PL.Add(n);


            }

        }
        else
        {


            foreach (GameObject n in OpenInvSlots)
            {
                GameObject T = n;
                for (int i = 1; i< temp.x; i++)
                {

                    if (CheckR(T))
                    {
                        T = T.GetComponent<InventoryGridNode>().right;

                    }
                    if(CheckR(T) == false)
                    {
                        break;
                    }

                }
                PL.Add(n);

            }
        }

    }

    private bool CheckR(GameObject node)
    {

        if (OpenInvSlots.Contains(node.GetComponent<InventoryGridNode>().right))
        {
            return true;

        }
        else
        {
            return false;
        }
    }



}
