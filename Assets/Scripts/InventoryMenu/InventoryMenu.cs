﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public List<InventoryGridNode> OpenNodes = new List<InventoryGridNode>();
    public List<InventoryGridNode> ToCheck = new List<InventoryGridNode>();
    public List<GameObject> EquipedItems = new List<GameObject>();

    public float damage = 0;
    public float range = 0;
    public float armor = 0;

    public List<GameObject> PL = new List<GameObject>();

    public Vector2 temp;

    int current;

    bool Active = false;

    private PlayerControl Player;
    private InventoryManager PlayerInv;

    public InventoryGridGen Grid;

    public InventoryObject selected;

    public List<GameObject> UIElements = new List<GameObject>();

    public GameObject Holder;

    public GameObject WeaponHand;

    public GameObject InventorySprite;

    public AudioEvent audioEvent;
    public AudioData inventoryOpenAudio;
    public AudioData inventoryCloseAudio;

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
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Active == false)
            {
                GetComponent<CanvasGroup>().alpha = 1;
                GetComponent<CanvasGroup>().interactable = true;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                Player.OpenMenu();
                Active = true;

                if (audioEvent != null)
                {
                    audioEvent.GetAudioController().PlayAudio(inventoryOpenAudio, gameObject);
                }

            }
            else if (Active == true)
            {
                GetComponent<CanvasGroup>().alpha = 0;
                GetComponent<CanvasGroup>().interactable = false;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
                Player.CloseMenu();
                Active = false;

                if (audioEvent != null)
                {
                    audioEvent.GetAudioController().PlayAudio(inventoryCloseAudio, gameObject);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {

            GetComponent<CanvasGroup>().alpha = 0;
            GetComponent<CanvasGroup>().interactable = false;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            Active = false;

        }
    }

    public bool AddItem(GameObject item)
    {
        temp = item.GetComponent<EquipableWeapon>().GetInvDim();
        ToCheck.Clear();

        foreach (InventoryGridNode Node in Grid.AllTiles)
        {
            if (Node.Contents == null && Node.Row <= Grid.Rows - (temp.y - 1) && Node.Column <= Grid.Columns - (temp.x - 1))
            {

                for (int c = 0; c < temp.x; c++)
                {
                    for (int r = 0; r < temp.y; r++)
                    {
                        ToCheck.Add(Grid.NodeAtCR(Node.Column + c, Node.Row + r));

                    }

                }
            }

            bool OpenSpot = false;
            foreach (InventoryGridNode inventoryGridNode in ToCheck)
            {
                OpenSpot = CheckNode(inventoryGridNode);

            }
            if (OpenSpot == true)
            {


                GameObject INVOBJ = Instantiate(InventorySprite, Node.transform.position, Quaternion.identity, Holder.transform);
                INVOBJ.GetComponent<InventoryObject>().RealObject = item;
                INVOBJ.GetComponent<InventoryObject>().dimensions = item.GetComponent<EquipableWeapon>().GetInvDim();
                INVOBJ.GetComponent<RectTransform>().sizeDelta = item.GetComponent<EquipableWeapon>().GetInvDim() * 50;
                INVOBJ.GetComponent<InventoryObject>().Last = Node.gameObject;
                INVOBJ.GetComponent<InventoryObject>().Holder = Holder;
                INVOBJ.GetComponent<Image>().sprite = item.GetComponent<EquipableWeapon>().InventorySprite;
                if (INVOBJ.GetComponent<InventoryObject>().ThisEquipment == EquipmentType.weapon)
                {

                    INVOBJ.GetComponent<InventoryObject>().WorldSlot = WeaponHand;
                }


                foreach (InventoryGridNode inventoryGridNode in ToCheck)
                {
                    inventoryGridNode.Contents = item;
                    INVOBJ.GetComponent<InventoryObject>().Location.Add(inventoryGridNode);
                    INVOBJ.GetComponent<InventoryObject>().menu = this;

                }
                Debug.Log("item picked up successfuly");
                //ToCheck.Clear();
                return true;

            }
            if (OpenSpot == false)
            {
                Debug.Log("checked " + Node.Column + "," + Node.Row + ", does not fit here");
                ToCheck.Clear();
            }
        }
        return false;
    }


    bool CheckNode(InventoryGridNode inventoryGridNode)
    {
        if (inventoryGridNode == null)
        {
            return false;
        }
        if (inventoryGridNode.Contents == null)
        {

            return true;
        }

        else
        {

            return false;
        }
    }

    public void DropItem(GameObject item)
    {
        PlayerInv.DropWeapon(item);

    }

    public void UpdateGearScore()
    {
        damage = 0;
        range = 0;
        armor = 0;
        foreach (GameObject E in EquipedItems)
        {
            damage += E.GetComponent<EquipableWeapon>().GetDamage();
            range += E.GetComponent<EquipableWeapon>().GetRange();
            armor += E.GetComponent<EquipableWeapon>().GetArmor();

        }
        Player.UpdateGearScore(range, damage, armor);

    }



}
