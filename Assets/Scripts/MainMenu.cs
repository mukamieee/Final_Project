using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);

        if (GameAudioManager.Instance != null)
            GameAudioManager.Instance.PlayButtonClick();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (GameAudioManager.Instance != null)
            GameAudioManager.Instance.PlayButtonClick();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

        if (GameAudioManager.Instance != null)
            GameAudioManager.Instance.PlayButtonClick();
    }

    public void QuitGame()
    {
        if (GameAudioManager.Instance != null)
            GameAudioManager.Instance.PlayButtonClick();

        Application.Quit();
        Debug.Log("Quit!");
    }
}