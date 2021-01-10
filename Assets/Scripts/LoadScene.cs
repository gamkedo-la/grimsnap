using System;
using PlayerDeath;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    private static string multipleLivesLabel = "remain";
    private static string singleLiveLabel = "remains";
    private static string _nextLiveScreen = "Next Live Screen";

    private static int livesToDisplay;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals(_nextLiveScreen))
        {
            Text numberLives = GameObject.Find("numberLives").GetComponent<Text>();
            numberLives.text = livesToDisplay.ToString();

            Text livesLabel = GameObject.Find("remain").GetComponent<Text>();
            if (livesToDisplay <= 1)
            {
                livesLabel.text = singleLiveLabel;
            }
            else
            {
                livesLabel.text = multipleLivesLabel;
            }
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Additive);
    }

    public void LoadWinScreenScene()
    {
        SceneManager.LoadScene("Win Screen");
    }

    public void LoadGameOverScreenScene()
    {
        SceneManager.LoadScene("Game Over Screen");
    }

    public void DisplayNextLifeScreenScene(int playerLives)
    {
        livesToDisplay = playerLives;
        SceneManager.LoadScene(_nextLiveScreen, LoadSceneMode.Additive);
    }

    public void HideNextLifeScreenScene()
    {
        Scene activeScene = SceneManager.GetSceneByName("Next Live Screen");
        SceneManager.UnloadSceneAsync(activeScene);
    }
    
}