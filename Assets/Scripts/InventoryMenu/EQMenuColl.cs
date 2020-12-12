using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQMenuColl : MonoBehaviour
{
    public GameObject Coll;
    Vector3 ColPos = new Vector3(0, 1000, 0);

    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {

        ColPos.x = GetComponent<RectTransform>().position.x;
        ColPos.z = GetComponent<RectTransform>().position.y;
        GameObject C = Instantiate(Coll, ColPos, Quaternion.identity, menu.transform);

        C.name = ("EQ Menu Collider");
        C.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
        C.GetComponent<BoxCollider>().size = new Vector3(GetComponent<RectTransform>().rect.width, 1, GetComponent<RectTransform>().rect.height);
        Coll = C;

        Coll.GetComponent<InventoryCollider>().rep = gameObject;
        Coll.tag = gameObject.tag;



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
