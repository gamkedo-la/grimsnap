﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    public GameObject RealObject;
    public List<InventoryGridNode> Location = new List<InventoryGridNode>();

    public InventoryMenu menu;

    public bool selected = false;

    Vector3 Offset;

    public Vector2 dimensions;

    public List<GameObject> Overlap = new List<GameObject>();
    public List<InventoryGridNode> survey = new List<InventoryGridNode>();

    public GameObject Coll;
    Vector3 ColPos = new Vector3(0, 1000, 0);
    public GameObject closest = null;

    public GameObject Last;

    public GameObject Holder;

    public EquipmentType ThisEquipment;

    public GameObject WorldSlot;

    public GameObject Stats;
    GameObject S;

    // Start is called before the first frame update
    void Start()
    {

        ColPos.x = GetComponent<RectTransform>().position.x;
        ColPos.z = GetComponent<RectTransform>().position.y;
        GameObject C = Instantiate(Coll, ColPos, Quaternion.identity, menu.transform);

        C.name = (RealObject.name + " inventory collider");
        Coll = C;
        Coll.GetComponent<InventoryCollider>().rep = gameObject;
        Coll.tag = gameObject.tag;
        Vector3 ts = Coll.transform.localScale;
        ts.z *= GetComponent<InventoryObject>().dimensions.y;
        ts.x *= GetComponent<InventoryObject>().dimensions.x;
        Coll.transform.localScale = ts;


    }

    // Update is called once per frame
    void Update()
    {
        
        if (selected == true)
        {

            transform.position = Input.mousePosition - Offset;
            
        }
        
    }

    public void ItemClicked()
    {

        float minDist = Mathf.Infinity;

        foreach (GameObject O in Overlap)
        {
            float dist = Vector3.Distance(transform.position, O.transform.position);
            if (dist < minDist)
            {
                closest = O;
                minDist = dist;
            }

        }


        //Debug.Log("clicked " + RealObject.name + " in inventory");
        if (selected == false && menu.selected == null)
        {
            transform.parent = menu.transform;
            selected = true;
            menu.selected = this;
            Offset = Input.mousePosition - transform.position;
            foreach (InventoryGridNode G in Location)
            {
                G.Contents = null;

            }
            Location.Clear();
            if(Last.tag == "EquipmentSlot")
            {
                Unequip();
                Last.GetComponent<EquipmentSlot>().Contents = null;
            }
        }
        else if (selected == true && menu.selected == this)
        {
            if(Overlap.Count == 0)
            {
                Debug.Log("dropping " + RealObject.name);
                menu.DropItem(RealObject);
                foreach (InventoryGridNode G in Location)
                {

                    G.Contents = gameObject;
                }
                Destroy(Coll);
                Destroy(gameObject);
                return;

            }

            foreach (GameObject O in Overlap)
            {
                if(O.tag == "EquipmentSlot" && 
                    O.GetComponent<EquipmentSlot>().thisSlot == ThisEquipment && 
                    O.GetComponent<EquipmentSlot>().Contents == null)
                {

                    Last = O;
                    selected = false;
                    menu.selected = null;

                    survey.Clear();


                    foreach (InventoryGridNode G in Location)
                    {
                        G.Contents = null;

                    }
                    Location.Clear();

                    Vector3 temp = O.GetComponent<RectTransform>().localPosition;
                    if (dimensions.x == 1)
                    {
                        temp.x += 25;
                    }
                    if (dimensions.y == 2)
                    {
                        temp.y -= 25;
                        
                    }
                    GetComponent<RectTransform>().localPosition = temp;

                    Last.GetComponent<EquipmentSlot>().Contents = RealObject;
                    Equip();
                    return;
                }

            }


            minDist = Mathf.Infinity;

            foreach (GameObject O in Overlap)
            {
                float dist = Vector3.Distance(transform.position, O.transform.position);
                if (dist < minDist)
                {
                    closest = O;
                    minDist = dist;
                }

            }
            

            for (int r = 0; r < dimensions.y; r++)
            {
                for (int c = 0; c < dimensions.x; c++)
                {
                    if (closest.GetComponent<InventoryGridNode>() != null)
                    {
                        survey.Add(menu.Grid.NodeAtCR(closest.GetComponent<InventoryGridNode>().Column + c, closest.GetComponent<InventoryGridNode>().Row + r));
                    }

                }
            }

            bool openSpot = true;
            if (survey.Count == 0)
            {

                openSpot = false;

            }
            else
            {
                foreach (InventoryGridNode I in survey)
                {

                    if (I.Contents != null)
                    {

                        openSpot = false;

                    }
                }
            }

            if (openSpot == true)
            {
                survey.Clear();
                transform.position = closest.transform.position;

                foreach (InventoryGridNode G in Location)
                {
                    G.Contents = null;

                }
                Location.Clear();


                for (int r = 0; r < dimensions.y; r++)
                {
                    for (int c = 0; c < dimensions.x; c++)
                    {
                        Location.Add(menu.Grid.NodeAtCR(closest.GetComponent<InventoryGridNode>().Column + c, closest.GetComponent<InventoryGridNode>().Row + r));
                    }
                }
                foreach (InventoryGridNode G in Location)
                {

                    G.Contents = gameObject;
                }
                transform.parent = Holder.transform;
                Last = closest;
                selected = false;
                menu.selected = null;

            }
            else
            {
                Debug.Log("does not fit");
                transform.parent = Holder.transform;
                selected = false;
                menu.selected = null;
                transform.position = Last.transform.position;
                survey.Clear();

                if (Last.tag != "EquipmentSlot")
                {
                    for (int r = 0; r < dimensions.y; r++)
                    {
                        for (int c = 0; c < dimensions.x; c++)
                        {
                            Location.Add(menu.Grid.NodeAtCR(Last.GetComponent<InventoryGridNode>().Column + c, Last.GetComponent<InventoryGridNode>().Row + r));
                        }
                    }
                    foreach (InventoryGridNode G in Location)
                    {

                        G.Contents = gameObject;
                    }
                }

                if (Last.tag == "EquipmentSlot")
                {
                    if (dimensions.x == 1)
                    {
                        Vector3 temp = Last.GetComponent<RectTransform>().localPosition;
                        temp.x += 25;
                        GetComponent<RectTransform>().localPosition = temp;
                    }
                    Last.GetComponent<EquipmentSlot>().Contents = RealObject;
                    Equip();
                }

                return;
            }
        }
    }

    public void AddOverlap(GameObject c)
    {

        Overlap.Add(c);

    }
    public void RemoveOverlap(GameObject c)
    {

        Overlap.Remove(c);

    }

    public void Equip()
    {

        RealObject.GetComponent<EquipableWeapon>().isEquiped = true;
        RealObject.GetComponent<SphereCollider>().enabled = false;
        RealObject.transform.position = WorldSlot.transform.position;
        RealObject.transform.rotation = WorldSlot.transform.rotation;
        RealObject.transform.parent = WorldSlot.transform;
        menu.EquipedItems.Add(RealObject);
        menu.UpdateGearScore();

    }

    public void Unequip()
    {
        RealObject.GetComponent<EquipableWeapon>().isEquiped = false;
        RealObject.GetComponent<SphereCollider>().enabled = true;
        RealObject.transform.position += new Vector3(0,-20,0);
        RealObject.transform.Rotate(Vector3.up, Random.Range(0, 360));
        RealObject.transform.parent = null;
        menu.EquipedItems.Remove(RealObject);
        menu.UpdateGearScore();

    }

    public void CreateStats()
    {
        S = Instantiate(Stats, transform.position, Quaternion.identity, transform);
        S.GetComponent<StatsPopup>().InfoSource = RealObject;
        transform.parent = menu.transform;
    }

    public void DestroyStats()
    {
        Destroy(S);
        transform.parent = Holder.transform;

    }
}
