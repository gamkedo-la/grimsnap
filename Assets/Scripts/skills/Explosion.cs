using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    float countUp = 0;
    public float explosionSpeed = 15;
    float b = 1;

    public float explosionLifetime = .6f;
    public float damage;

    // Update is called once per frame
    void Update()
    {

        countUp += Time.deltaTime;
        b += Time.deltaTime * explosionSpeed;
        Vector3 s = new Vector3(b, b, b);

        transform.localScale = s;

        if (countUp > explosionLifetime)
        {

            Destroy(gameObject);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {

            other.gameObject.GetComponent<Health>().TakeDamage(damage);

        }
    }


}
