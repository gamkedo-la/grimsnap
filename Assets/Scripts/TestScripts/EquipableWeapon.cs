using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableWeapon : MonoBehaviour
{

    private float range;
    private int damage;


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

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerControl>().pickUpTarget == gameObject)
        {
            Debug.Log(name + " getting picked up");
            collision.gameObject.GetComponent<InventoryManager>().CollectWeapon(gameObject);
            transform.position += (Vector3.down * 20);
        }

    }

}
