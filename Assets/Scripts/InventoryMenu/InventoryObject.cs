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
    public List<InventoryGridNode> survey = new List<InventoryGridNode>();

    public GameObject Coll;
    Vector3 ColPos = new Vector3(0, 1000, 0);
    public GameObject closest = null;

    public GameObject Last;

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

            selected = true;
            menu.selected = this;
            Offset = Input.mousePosition - transform.position;
            foreach (InventoryGridNode G in Location)
            {
                G.Contents = null;

            }
            Location.Clear();
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

                Last = closest;
                selected = false;
                menu.selected = null;

            }
            else
            {
                Debug.Log("does not fit");
                selected = false;
                menu.selected = null;
                transform.position = Last.transform.position;
                survey.Clear();
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
}
