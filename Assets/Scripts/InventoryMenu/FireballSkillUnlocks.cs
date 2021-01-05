using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkillUnlocks : MonoBehaviour
{

    public FireballShooter Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<FireballShooter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockFireball()
    {
        Player.unlocked = true;

    }

    public void UnlockSplit()
    {
        Player.split = true;

    }

    public void UnlockTrail()
    {
        Player.trail = true;

    }

    public void UnlockExplosion()
    {
        Player.explode = true;

    }

    public void UnlockBurnEnemies()
    {
        Player.burn = true;

    }

}
