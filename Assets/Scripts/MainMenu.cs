using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Level 1 is Scene 1 (Scene 0 is Main Menu)
        SceneManager.LoadScene(1);

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