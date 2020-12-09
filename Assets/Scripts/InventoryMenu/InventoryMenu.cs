using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public List<InventoryGridNode> OpenNodes = new List<InventoryGridNode>();
    public List<InventoryGridNode> ToCheck = new List<InventoryGridNode>();

    public List<GameObject> PL = new List<GameObject>();

    public Vector2 temp;

    public GameObject InvSpritePrefab;

    int current;

    bool Active = false;

    private PlayerControl Player;
    private InventoryManager PlayerInv;

    InventoryGridGen Grid;

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        PlayerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        Grid = GetComponent<InventoryGridGen>();

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

    public bool AddItem(GameObject item)
    {
        temp = item.GetComponent<EquipableWeapon>().GetInvDim();
        ToCheck.Clear();

        foreach(InventoryGridNode Node in Grid.AllTiles)
        {
            if(Node.Contents == null && Node.Row <= Grid.Rows - (temp.y-1) && Node.Column <= Grid.Columns - (temp.x-1))
            {

                for(int c = 0; c< temp.x; c++)
                {
                    for (int r = 0; r<temp.y; r++)
                    {
                        ToCheck.Add(Grid.NodeAtCR(Node.Column + c, Node.Row + r));

                    }
                                       
                }
            }

            bool OpenSpot = false;
            foreach(InventoryGridNode inventoryGridNode in ToCheck)
            {
                OpenSpot = CheckNode(inventoryGridNode);

            }
            if(OpenSpot == true)
            {
                Instantiate(item.GetComponent<EquipableWeapon>().InventorySprite, Node.transform.position, Quaternion.identity, transform);

                foreach (InventoryGridNode inventoryGridNode in ToCheck)
                {
                    inventoryGridNode.Contents = item;

                }
                Debug.Log("item picked up successfuly");
                return true;

            }
            if(OpenSpot == false)
            {
                Debug.Log("checked " + Node.Column + "," + Node.Row + ", does not fit here");
                ToCheck.Clear();
            }
        }
        return false;
    }


    bool CheckNode(InventoryGridNode inventoryGridNode)
    {
        if(inventoryGridNode == null)
        {
            return false;
        }
        if(inventoryGridNode.Contents == null)
        {

            return true;
        }

        else
        {

            return false;
        }
    }

    public void DropItem(int slot)
    {


    }

   

}
