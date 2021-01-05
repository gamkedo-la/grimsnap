using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{

    public SkillButton PreReq;

    public bool unlocked;

    PlayerLevel PlayerL;

    public SkillsMenu skillsMenu;

    public int skillCost = 1;


    // Start is called before the first frame update
    void Start()
    {
        PlayerL = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        GetComponent<Button>().interactable = false;
        if(PlayerL.SkillPoints < skillCost)
        {
            return;

        }

        if(PreReq == null && PlayerL.SkillPoints >= skillCost && unlocked == false)
        {
            GetComponent<Button>().interactable = true;
            return;


        }
        if(PreReq != null && PreReq.unlocked == true && PlayerL.SkillPoints >= skillCost && unlocked == false)
        {
            GetComponent<Button>().interactable = true;
            return;

        }



    }

    public void SelectedSkill()
    {
        GetComponent<Button>().interactable = false;
        unlocked = true;
        
        PlayerL.unlockSkill(skillCost);
        skillsMenu.RefreshButtons();

    }

    


}
