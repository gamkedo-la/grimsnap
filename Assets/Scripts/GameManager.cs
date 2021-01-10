using System.Collections;
using System.Collections.Generic;
using PlayerDeath;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private Health playerHealth;
    private LoadScene loadScene;
    private Lives playerLives;
    private ResetLocations bonfires;

    private bool isMainSceneInBackground = false;

    void Start()
    {
        playerHealth = player.GetComponent<Health>();
        loadScene = GetComponent<LoadScene>();
        playerLives = GetComponent<Lives>();
        bonfires = GetComponent<ResetLocations>();
    }

    void Update()
    {
        if (!isMainSceneInBackground)
        {
            if (playerHealth.GetHealth() <= 0)
            {
                playerLives.LooseLive();
                if (playerLives.IsNoLivesLeft())
                {
                    loadScene.LoadGameOverScreenScene();
                }
                else
                {
                    ShowLivesThenReset();
                }
            }

            if (player.GetComponent<InventoryManager>().GetCountOfQuestItems() >= 3)
            {
                loadScene.LoadWinScreenScene();
            }
        }
    }

    private void ShowLivesThenReset()
    {
        if(!isMainSceneInBackground)
        {
            isMainSceneInBackground = true;
            loadScene.DisplayNextLifeScreenScene(playerLives.lives);
            Invoke("LoadLevelAgain", 3f);
        }
    }

    private void LoadLevelAgain()
    {
        isMainSceneInBackground = false;
        loadScene.HideNextLifeScreenScene();
        playerHealth.Refill();
        bonfires.ResetPlayer();
    }


}