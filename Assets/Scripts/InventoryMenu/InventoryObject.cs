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


    // Start is called before the first frame update
    void Start()
    {

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
        if(selected == false && menu.selected == null)
        {

            selected = true;
            menu.selected = this;
            Offset = Input.mousePosition - transform.position;
            Overlap.Clear();
        }
        else if (selected == true  && menu.selected == this)
        {

            selected = false;
            menu.selected = null;

            foreach(GameObject E in menu.UIElements)
            {
                if (RectOverlaps( E.GetComponent<RectTransform>(), GetComponent<RectTransform>()))
                {
                    Overlap.Add(E);

                }

            }
        }

    }

    bool RectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.position.x, rectTrans1.position.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.position.x, rectTrans2.position.y, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);

    }
}
