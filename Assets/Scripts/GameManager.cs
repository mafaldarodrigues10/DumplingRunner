using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameOver = false;

    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI runCoinsText;

    public ChaserFollow chaser;

    public float gameOverDelay = 2f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isGameOver = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        if (CameraFollow.instance != null)
        {
            CameraFollow.instance.ShakeCamera();
        }

        if (chaser != null)
        {
            chaser.StartCatch();
        }

        Debug.Log("Game Over!");

        SaveScoreAndCoins();
        UpdateGameOverTexts();

        StartCoroutine(ShowGameOverAfterDelay());
    }

    void SaveScoreAndCoins()
    {
        if (ScoreManager.instance == null) return;

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (ScoreManager.instance.score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", ScoreManager.instance.score);
        }

        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += ScoreManager.instance.runCoins;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);

        PlayerPrefs.Save();
    }

    void UpdateGameOverTexts()
    {
        if (ScoreManager.instance == null) return;

        if (finalScoreText != null)
            finalScoreText.text = "Score: " + ScoreManager.instance.score;

        if (runCoinsText != null)
            runCoinsText.text = "Coins: " + ScoreManager.instance.runCoins;
    }

    IEnumerator ShowGameOverAfterDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);

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
        if (!isGameOver)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
}