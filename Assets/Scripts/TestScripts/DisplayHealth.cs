using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    Health health;
    Image healthImage;

    private void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        healthImage = GetComponent<Image>();
    }

    private void Update()
    {
        healthImage.fillAmount = health.health / health.maxHealth;
    }
}
