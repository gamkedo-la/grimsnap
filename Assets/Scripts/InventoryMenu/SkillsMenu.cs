using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsMenu : MonoBehaviour
{

    bool Active = false;
    private PlayerControl Player;

    public List<SkillButton> skillButtons = new List<SkillButton>();

    public AudioEvent audioEvent;
    public AudioData skillOpenAudio;
    public AudioData skillCloseAudio;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Active == false)
            {
                GetComponent<CanvasGroup>().alpha = 1;
                GetComponent<CanvasGroup>().interactable = true;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                Player.OpenMenu();
                Active = true;
                RefreshButtons();

                if (audioEvent != null)
                {
                    audioEvent.GetAudioController().PlayAudio(skillOpenAudio, gameObject);
                }

            }
            else if (Active == true)
            {
                GetComponent<CanvasGroup>().alpha = 0;
                GetComponent<CanvasGroup>().interactable = false;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
                Player.CloseMenu();
                Active = false;

                if (audioEvent != null)
                {
                    audioEvent.GetAudioController().PlayAudio(skillCloseAudio, gameObject);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {

            GetComponent<CanvasGroup>().alpha = 0;
            GetComponent<CanvasGroup>().interactable = false;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            Active = false;

        }

    }

    public void RefreshButtons()
    {


        foreach (SkillButton SB in skillButtons)
        {

            SB.Activate();

        }

    }
}
