using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    Health health;
    Slider slider;

    private void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        Debug.Log("Reporting for duty");
        slider.value = health.health / health.maxHealth;
    }
}
