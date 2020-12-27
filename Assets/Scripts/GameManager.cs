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

    void Start()
    {
        playerHealth = player.GetComponent<Health>();
        loadScene = GetComponent<LoadScene>();
        playerLives = GetComponent<Lives>();
        bonfires = GetComponent<ResetLocations>();
    }

    void Update()
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
                playerHealth.Refill();
                bonfires.ResetPlayer();
            }
        }

        if (player.GetComponent<InventoryManager>().GetCountOfQuestItems() >= 3)
        {
            loadScene.LoadWinScreenScene();
        }
    }
}