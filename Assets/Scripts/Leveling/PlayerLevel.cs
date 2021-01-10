using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{

    public float EXP = 0;
    public float NextLevelEXP = 100;
    public int CurrentLevel = 1;

    public int SkillPoints = 0;

    public Text MenuSkillPoints;

    public Text MenuLevelDisplay;

    public Slider expBar;

    public GameObject LevelUpMessage;

    // Start is called before the first frame update
    void Start()
    {

        MenuSkillPoints.text = SkillPoints.ToString();

        MenuLevelDisplay.text = CurrentLevel.ToString();

        expBar.value = (EXP / NextLevelEXP);

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

        MenuLevelDisplay.text = CurrentLevel.ToString();
        MenuSkillPoints.text = SkillPoints.ToString();

        LevelUpMessage.GetComponent<LevelPopup>().Level = CurrentLevel;
        LevelUpMessage.GetComponent<LevelPopup>().DisplayPopup();

    }

    public void GainEXP(int xp)
    {

        EXP += xp;
        if(EXP >= NextLevelEXP)
        {

            LevelUp();
        }
        expBar.value = (EXP / NextLevelEXP);
    }

    public void unlockSkill (int cost)
    {

        SkillPoints -= cost;

        MenuSkillPoints.text = SkillPoints.ToString();
        

    }

}
