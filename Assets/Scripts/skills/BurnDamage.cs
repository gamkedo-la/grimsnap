using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDamage : MonoBehaviour
{

    public float damage;
    public float burnInterval;
    float burnReset;

    public float lifetime;

    private void Start()
    {
        burnReset = burnInterval;
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {

            Destroy(gameObject);
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null){
            health.TakeDamage(damage);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        burnInterval -= Time.deltaTime;
        if(burnInterval <= 0)
        {
            Health health = other.gameObject.GetComponent<Health>();
            if (health != null){
                health.TakeDamage(damage);
                burnInterval = burnReset;
            }


        }

    }
}
