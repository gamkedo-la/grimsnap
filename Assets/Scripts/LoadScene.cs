using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    private const string MultipleLivesLabel = "remain";
    private const string SingleLiveLabel = "remains";
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public GameObject gameUi;
    public GameObject nextLifePanel;
    public GameObject introStoryPanel;

    public GameObject numberLivesTextPart;
    public GameObject livesRemainTextPart;

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title Screen");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
        Debug.Log("loaded Main scene");
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits Screen");
    }

    public void DisplayWinScreen()
    {
        gameUi.SetActive(false);
        winPanel.SetActive(true);
    }

    public void DisplayGameOverScreen()
    {
        gameUi.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void DisplayNextLifeScreen(int playerLives)
    {
        UpdateNextLifeInfo(playerLives);
        gameUi.SetActive(false);
        nextLifePanel.SetActive(true);
    }

    private void UpdateNextLifeInfo(int playerLives)
    {
        var livesToDisplay = playerLives;

        var _numberLives = numberLivesTextPart.GetComponent<Text>();
        var _livesLabel = livesRemainTextPart.GetComponent<Text>();

        _numberLives.text = livesToDisplay.ToString();
        _livesLabel.text = livesToDisplay <= 1 ? SingleLiveLabel : MultipleLivesLabel;
    }

    public void HideNextLifeScreenScene()
    {
        gameUi.SetActive(true);
        nextLifePanel.SetActive(false);
    }

    public void HideIntroStoryScreen()
    {
        gameUi.SetActive(true);
        introStoryPanel.SetActive(false);
    }
}