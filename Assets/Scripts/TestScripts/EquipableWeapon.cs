using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableWeapon : MonoBehaviour
{

    private float range = 0;
    private int damage = 0;
    private float armor = 0;

    private Vector2 InvDimensions = new Vector2(0, 0);

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
        return damage;

    }

    public void SetRange(float R)
    {
         range = R;

    }

    public void SetDamage(int D)
    {
        damage = D;

    }

    public void SetArmor(float A)
    {
        armor = A;

    }

    public float GetArmor()
    {
        return armor;

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
