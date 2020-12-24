using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject burnPatch;
    public GameObject miniFireball;

    public bool split = false;
    public bool trail = false;
    public bool explode = false;
    public bool burn = false;

    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
