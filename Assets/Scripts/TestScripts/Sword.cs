using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public int minDamage;
    public int maxDamage;
    public float range;

    private int damageFinal;



    // Start is called before the first frame update
    void Start()
    {
        damageFinal = Random.Range(minDamage, maxDamage + 1);

        EquipableWeapon thisWeapon = GetComponent<EquipableWeapon>();

        thisWeapon.SetDamage(damageFinal);
        thisWeapon.SetRange(range);



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
