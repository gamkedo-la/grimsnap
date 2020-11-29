using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadWinScreenScene()
    {
        SceneManager.LoadScene("Win Screen");
    }

    public void LoadGameOverScreenScene()
    {
        SceneManager.LoadScene("Game Over Screen");
    }
}