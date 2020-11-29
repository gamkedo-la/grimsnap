using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Collectable> questItems = new List<Collectable>();

    public void CollectQuestItem(Collectable questItem)
    {
        questItems.Add(questItem);
    }

    public float GetCountOfQuestItems()
    {
        return questItems.Count;
    }
}