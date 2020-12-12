using System.Collections;
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

    public GameObject Coll;
    Vector3 ColPos = new Vector3(0, 1000, 0);


    // Start is called before the first frame update
    void Start()
    {

        ColPos.x = GetComponent<RectTransform>().position.x;
        ColPos.z = GetComponent<RectTransform>().position.y;
        GameObject C = Instantiate(Coll, ColPos, Quaternion.identity, menu.transform);
        C.name = (RealObject.name + " inventory collider");
        Coll = C;
        Coll.tag = gameObject.tag;
        Coll.GetComponent<InventoryCollider>().rep = gameObject;

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
        Debug.Log("clicked " + RealObject.name + " in inventory");
        if (selected == false && menu.selected == null)
        {

            selected = true;
            menu.selected = this;
            Offset = Input.mousePosition - transform.position;
            Overlap.Clear();
        }
        else if (selected == true && menu.selected == this)
        {


            float minDist = Mathf.Infinity;
            GameObject closest = null;
            foreach (GameObject O in Overlap)
            {
                float dist = Vector3.Distance(transform.position, O.transform.position);
                if (dist < minDist)
                {
                    closest = O;
                    minDist = dist;
                }

            }
            transform.position = closest.transform.position;

            List<InventoryGridNode> survey = new List<InventoryGridNode>();

            for (int r = 0; r < dimensions.y; r++)
            {
                for (int c = 0; c < dimensions.x; c++)
                {
                    survey.Add(menu.Grid.NodeAtCR(closest.GetComponent<InventoryGridNode>().Column + c, closest.GetComponent<InventoryGridNode>().Row + r));
                }
            }

            bool openSpot = true;

            foreach(InventoryGridNode I in survey)
            {

                if(I.Contents != null)
                {
                    openSpot = false;
                }
            }

            if (openSpot == true)
            {
                


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

                selected = false;
                menu.selected = null;
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
}
