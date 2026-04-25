using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameOver = false;

    [Header("UI")]
    public GameObject inGameUI;
    public GameObject gameOverPanel;

    [Header("Game Over Texts")]
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI runCoinsText;

    [Header("Chaser")]
    public ChaserFollow chaser;
    public float gameOverDelay = 2f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isGameOver = false;

        if (inGameUI != null)
            inGameUI.SetActive(true);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayHit();
        }

        if (inGameUI != null)
            inGameUI.SetActive(false);

        if (CameraFollow.instance != null)
            CameraFollow.instance.ShakeCamera();

        if (chaser != null)
            chaser.StartCatch();

        SaveScoreAndCoins();

        StartCoroutine(ShowGameOverAfterDelay());
    }

    void SaveScoreAndCoins()
    {
        if (ScoreManager.instance == null) return;

        int score = ScoreManager.instance.score;
        int coinsThisRun = ScoreManager.instance.runCoins;

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (score > bestScore)
            PlayerPrefs.SetInt("BestScore", score);

        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        PlayerPrefs.SetInt("TotalCoins", totalCoins + coinsThisRun);

        PlayerPrefs.Save();
    }

    void UpdateGameOverTexts()
    {
        if (ScoreManager.instance == null) return;

        int score = ScoreManager.instance.score;
        int coinsThisRun = ScoreManager.instance.runCoins;

        if (finalScoreText != null)
            finalScoreText.text = "Score: " + score;

        if (runCoinsText != null)
            runCoinsText.text = "Coins: " + coinsThisRun;
    }

    IEnumerator ShowGameOverAfterDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);

        UpdateGameOverTexts();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        StartMenuManager.startDirectly = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        StartMenuManager.startDirectly = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (!isGameOver) return;

        if (Input.GetKeyDown(KeyCode.R))
            RestartGame();
    }
}