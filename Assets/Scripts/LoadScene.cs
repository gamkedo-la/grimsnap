using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
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
    
    public void DisplayNextLifeScreenScene()
    {
        SceneManager.LoadScene("Next Live Screen", LoadSceneMode.Additive);
    }
    
    public void HideNextLifeScreenScene()
    {
        Scene activeScene = SceneManager.GetSceneByName("Next Live Screen");
        SceneManager.UnloadSceneAsync(activeScene);
    }
}