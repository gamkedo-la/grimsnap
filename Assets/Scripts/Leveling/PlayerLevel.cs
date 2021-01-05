using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{

    public int EXP = 0;
    public int NextLevelEXP = 100;
    public int CurrentLevel = 1;

    public int SkillPoints = 0;

    public Text MenuSkillPoints;

    // Start is called before the first frame update
    void Start()
    {

        MenuSkillPoints.text = SkillPoints.ToString();

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

    public void unlockSkill (int cost)
    {

        SkillPoints -= cost;

        MenuSkillPoints.text = SkillPoints.ToString();

    }

}
