using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Collectable> questItems = new List<Collectable>();

    private List<GameObject> weaponsInInv = new List<GameObject>();

    private GameObject InventoryMenu;

    public int InventorySize = 8;

    private void Start()
    {
        InventoryMenu = GameObject.Find("InventoryMenu");
    }

    public void CollectQuestItem(Collectable questItem)
    {
        questItems.Add(questItem);
    }

    public float GetCountOfQuestItems()
    {
        return questItems.Count;
    }

    public bool CollectWeapon(GameObject weapon)
    {
        GetComponent<PlayerControl>().pickUpTarget = null;

        Debug.Log("adding to inventory");
        
        return InventoryMenu.GetComponent<InventoryMenu>().AddItem(weapon);

    }

    public void DropWeapon(GameObject weapon)
    {

        weaponsInInv.Remove(weapon);
        weapon.transform.position = transform.position + new Vector3(Random.Range(.03f, .05f), -0.8f, Random.Range(.03f, .05f));

    }

    public int GetCountOfWeapons()
    {
        return weaponsInInv.Count;

    }
}