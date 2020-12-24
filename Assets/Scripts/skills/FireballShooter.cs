using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballShooter : MonoBehaviour
{

    public bool unlocked = false;

    public GameObject fireballPrefab;
    RaycastHit raycastHit;
    RaycastHit click;

    public bool split = false;
    public bool trail = false;
    public bool explode = false;
    public bool burn = false;

    public Vector3 temp = new Vector3(0,0,0);

    float cooldown = 0;
    public float cooldownReset =10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (unlocked == true)
        {
            cooldown -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.F) && cooldown <= 0)
            {

                ShootFireball();
                cooldown = cooldownReset;
            }
        }
    }

    public void ShootFireball()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out click, 100);

        temp = click.point;
        temp.y = transform.position.y;

        GameObject F = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        F.transform.LookAt(temp);

        F.GetComponent<Fireball>().split = split;
        F.GetComponent<Fireball>().trail = trail;
        F.GetComponent<Fireball>().burn = burn;
        F.GetComponent<Fireball>().explode = explode;
        F.GetComponent<Fireball>().ignore = gameObject;


    }

}
