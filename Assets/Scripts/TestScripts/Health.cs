using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public bool isDead = false;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Taking Damage" + transform.name);
        health -= damage;
    }
}
