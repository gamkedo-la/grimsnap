using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public int minDamage;
    public int maxDamage;
    public float range;
    public float armor;

    private int damageFinal;

    public Vector2 InvDimensions = new Vector2(1, 3);

    public GameObject INV;

    public string SwordName;

    // Start is called before the first frame update
    void Start()
    {
        damageFinal = Random.Range(minDamage, maxDamage + 1);

        EquipableWeapon thisWeapon = GetComponent<EquipableWeapon>();

        thisWeapon.SetDamage(damageFinal);
        thisWeapon.SetRange(range);
        thisWeapon.SetArmor(armor);
        thisWeapon.SetInvDim(InvDimensions);
        thisWeapon.InventorySprite = INV;
        thisWeapon.ItemName = SwordName;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
