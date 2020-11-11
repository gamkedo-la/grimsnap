using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;

    private void Awake()
    {
        health = maxHealth;
    }

}
