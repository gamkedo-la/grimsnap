using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private Health playerHealth;
    private LoadScene loadScene;

    void Start()
    {
        playerHealth = player.GetComponent<Health>();
        loadScene = GetComponent<LoadScene>();
    }

    void Update()
    {
        if (playerHealth.GetHealth() <= 0)
        {
            loadScene.LoadGameOverScreenScene();
        }

        if (player.GetComponent<InventoryManager>().GetCountOfQuestItems() >= 3)
        {
            loadScene.LoadWinScreenScene();
        }
    }
}