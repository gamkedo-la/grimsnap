using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableWeapon : MonoBehaviour
{

    private float range;
    private int damage;

    private Vector2 InvDimensions = new Vector2(0, 0);

    private Sprite InventorySprite;


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

    public void SetInvDim(Vector2 d)
    {

        InvDimensions = d;
    }

    public Vector2 GetInvDim()
    {

        return InvDimensions;

    }

    public void SetInvSprite(Sprite S)
    {
        InventorySprite = S;

    }


    public Sprite GetInvSprite()
    {

        return InventorySprite;
    }


}
