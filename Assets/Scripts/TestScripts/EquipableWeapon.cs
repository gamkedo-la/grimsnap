using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableWeapon : MonoBehaviour
{

    public float range = 0;
    public int minDamage;
    public int maxDamage;
    public float minArmor;
    public float maxArmor;

    private int damageFinal;
    private float armorFinal;

    public Vector2 InvDimensions = new Vector2(0, 0);

    public GameObject InventorySprite;

    public bool isEquiped = false;

    public string ItemName;

    private void Start()
    {
        if (ItemName == null)
        {
            ItemName = gameObject.name;

        }
    }

    public float GetRange()
    {
        return range;

    }

    public int GetDamage()
    {
        return damageFinal;

    }

    public void SetRange(float R)
    {
         range = R;

    }



    public float GetArmor()
    {
        return armorFinal;

    }

    public void SetInvDim(Vector2 d)
    {

        InvDimensions = d;
    }

    public Vector2 GetInvDim()
    {

        return InvDimensions;

    }




}
