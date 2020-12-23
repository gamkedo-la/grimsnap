using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{

    public int EXP = 0;
    public int NextLevelEXP = 100;
    public int CurrentLevel = 1;

    public int SkillPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp()
    {

        EXP -= NextLevelEXP;
        float temp = NextLevelEXP * 1.1f;
        NextLevelEXP = (int)temp;
        CurrentLevel++;
        SkillPoints++;

    }

    public void GainEXP(int xp)
    {

        EXP += xp;
        if(EXP >= NextLevelEXP)
        {

            LevelUp();
        }

    }

}
