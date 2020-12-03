using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvSlotMenu : MonoBehaviour
{
    GameObject n;
    GameObject d;
    GameObject r;

    private int slot;

    public GameObject WeaponRef;

    // Start is called before the first frame update
    void Start()
    {
        n = transform.Find("Name").gameObject;
        d = transform.Find("damage").gameObject;
        r = transform.Find("Range").gameObject;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Populate(GameObject w, int s)
    {
        WeaponRef = w;
        n.GetComponent<Text>().text = w.name;
        d.GetComponent<Text>().text = w.GetComponent<EquipableWeapon>().GetDamage().ToString();
        r.GetComponent<Text>().text = w.GetComponent<EquipableWeapon>().GetRange().ToString();


    }

}
