using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text levelText;
    public TMP_Text dotsRemainingText;
    public TMP_Text powerUpText;

    [Header("Settings")]
    public int pointsToLevelUp = 50;

    private int score = 0;
    private int level = 1;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
        HidePowerUpText();
    }

    void Update()
    {
        // Update dots remaining every frame
        if (dotsRemainingText != null)
        {
            Dot[] dots = FindObjectsByType<Dot>(FindObjectsSortMode.None);
            dotsRemainingText.text = "Coins: " + dots.Length;
        }
    }

    public void AddPoints(int points)
    {
        score += points;

        if (score >= level * pointsToLevelUp)
            LevelUp();

        UpdateUI();
    }

    void LevelUp()
    {
        level++;
        Debug.Log($"Level Up! Now level {level}");

        // Speed up enemy
        EnemyAI enemy = FindFirstObjectByType<EnemyAI>();
        if (enemy != null)
            enemy.IncreaseSpeed(0.5f);

        UpdateUI();
    }

    public void ShowPowerUpText(string message, float duration)
    {
        if (powerUpText != null)
        {
            powerUpText.text = message;
            powerUpText.gameObject.SetActive(true);
            Invoke(nameof(HidePowerUpText), duration);
        }
    }

    void HidePowerUpText()
    {
        if (powerUpText != null)
            powerUpText.gameObject.SetActive(false);
    }

    public int GetScore() { return score; }
    public int GetLevel() { return level; }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        if (levelText != null)
            levelText.text = "Level: " + level;
    }
}