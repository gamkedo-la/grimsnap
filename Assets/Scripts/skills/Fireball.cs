using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject burnPatch;
    public GameObject ignore;
    public GameObject explosion;

    public bool split = false;
    public bool trail = false;
    public bool explode = false;
    public bool burn = false;

    public float speed = 1.0f;

    public float burnTimer = .7f;
    float burnReset;

    public float damage;

    public float lifeTime;
    public float splitLife;

    // Start is called before the first frame update
    void Start()
    {
        burnReset = burnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (trail == true)
        {
            burnTimer -= Time.deltaTime;

            if (burnTimer <= 0)
            {
                Instantiate(burnPatch, transform.position, Quaternion.identity);
                burnTimer = burnReset;

            }
        }

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("fireball hit " + other.gameObject.name);
        if(other.gameObject.tag == "Enemy" && ignore != other.gameObject)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);

            if (split == true)
            {

                GameObject M = Instantiate(gameObject, transform.position, transform.rotation);
                M.transform.Rotate(Vector3.up, 45.0f);

                M.GetComponent<Fireball>().split = false;
                M.GetComponent<Fireball>().lifeTime = splitLife;
                M.GetComponent<Fireball>().ignore = other.gameObject;



                M = Instantiate(gameObject, transform.position, transform.rotation);

                M.GetComponent<Fireball>().split = false;
                M.GetComponent<Fireball>().lifeTime = splitLife;
                M.GetComponent<Fireball>().ignore = other.gameObject;



                M = Instantiate(gameObject, transform.position, transform.rotation);
                M.transform.Rotate(Vector3.up, -45.0f);

                M.GetComponent<Fireball>().split = false;
                M.GetComponent<Fireball>().lifeTime = splitLife;
                M.GetComponent<Fireball>().ignore = other.gameObject;

            }
            if (burn == true)
            {
                Instantiate(burnPatch, other.transform.position, Quaternion.identity, other.transform);

            }
            if(explode == true)
            {

                GameObject E = Instantiate(explosion, transform.position, Quaternion.identity);
                E.GetComponent<Explosion>().damage = damage;
            }

            

            Destroy(gameObject);

        }

    }
}
