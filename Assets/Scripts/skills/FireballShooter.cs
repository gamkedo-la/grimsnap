using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballShooter : MonoBehaviour
{

    public GameObject fireballPrefab;
    RaycastHit raycastHit;
    RaycastHit click;

    public bool split = false;
    public bool trail = false;
    public bool explode = false;
    public bool burn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootFireball()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out click, 100);

        GameObject F = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        Vector3 temp = click.point;
        temp.z = transform.position.z;

        F.transform.LookAt(temp);

        F.GetComponent<Fireball>().split = split;
        F.GetComponent<Fireball>().trail = trail;
        F.GetComponent<Fireball>().burn = burn;
        F.GetComponent<Fireball>().explode = explode;


    }

}
