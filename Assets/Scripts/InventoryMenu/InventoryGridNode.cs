using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGridNode : MonoBehaviour
{

    public GameObject Contents;
    public int Column;
    public int Row;

    public GameObject Grid;

    public GameObject Coll;
    Vector3 ColPos = new Vector3(0, 1000, 0);

    // Start is called before the first frame update
    void Start()
    {
        ColPos.x = GetComponent<RectTransform>().position.x;
        ColPos.z = GetComponent<RectTransform>().position.y;
        GameObject C = Instantiate(Coll, ColPos, Quaternion.identity, Grid.transform);
        C.name = (Column + " " + Row + " Collider");
        Coll = C;
        Coll.tag = gameObject.tag;
        Coll.GetComponent<InventoryCollider>().rep = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
