using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPopup : MonoBehaviour
{

    public GameObject InfoSource;

    private EquipableWeapon thisWeapon;

    public Text N;
    public Text[] properties;

    public int bookmark = 0;

    // Start is called before the first frame update
    void Start()
    {

        N.text = InfoSource.name;
        thisWeapon = InfoSource.GetComponent<EquipableWeapon>();

        if(thisWeapon.GetDamage() != 0)
        {
            properties[bookmark].text = "Damage: " + thisWeapon.GetDamage();
            bookmark++;

        }
        if (thisWeapon.GetRange() != 0)
        {
            properties[bookmark].text = "Range: " + thisWeapon.GetRange();
            bookmark++;

        }
        if (thisWeapon.GetArmor() != 0)
        {
            properties[bookmark].text = "Armor: " + thisWeapon.GetArmor();
            bookmark++;

        }

        //add something here for other properties if they ever come up

        GetComponent<RectTransform>().sizeDelta = new Vector2(100, (bookmark * 17) + 22);
        

        for(int x = bookmark; x< properties.Length; x++)
        {

            properties[x].text = " ";

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
