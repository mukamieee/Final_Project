using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public bool isGameOver = false;

    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject pausePanel;

    [Header("UI Text")]
    public TMP_Text finalScoreText;

    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        // Pause with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGameOver)
                TogglePause();
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Show final score
        if (finalScoreText != null)
            finalScoreText.text = "Score: " +
                ScoreManager.Instance.GetScore();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
        Debug.Log("Game Over!");
    }

    public void LoadNextLevel()
    {
        if (isGameOver) return;
        isGameOver = true;

        Time.timeScale = 1f;
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            // No more levels — show win screen
            if (winPanel != null)
                winPanel.SetActive(true);

            if (finalScoreText != null)
                finalScoreText.text = "Final Score: " +
                    ScoreManager.Instance.GetScore();

            Time.timeScale = 0f;
            Debug.Log("You completed the game!");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        SceneManager.LoadScene(0);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pausePanel != null)
            pausePanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
        Debug.Log(isPaused ? "Game Paused" : "Game Resumed");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}