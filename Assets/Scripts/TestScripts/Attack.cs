using MyMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Ony responsible for doing damage to target, not checking whether or not you can
    public void AttackTarget(Health target, float damage)
    {
        target.TakeDamage(damage);
    }
}
