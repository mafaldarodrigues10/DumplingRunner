using UnityEngine;
using TMPro;

public class StartMenuManager : MonoBehaviour
{
    public static bool startDirectly = false;

    public GameObject startMenu;
    public GameObject inGameUI;
    public GameObject gameOverPanel;

    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI totalCoinsText;

    void Start()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        if (bestScoreText != null)
            bestScoreText.text = "Best: " + bestScore;

        if (totalCoinsText != null)
            totalCoinsText.text = totalCoins.ToString();

        if (startDirectly)
        {
            StartGame();
            startDirectly = false;
        }
        else
        {
            if (startMenu != null) startMenu.SetActive(true);
            if (inGameUI != null) inGameUI.SetActive(false);
            if (gameOverPanel != null) gameOverPanel.SetActive(false);

            Time.timeScale = 0f;
        }

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void StartGame()
    {
        if (startMenu != null) startMenu.SetActive(false);
        if (inGameUI != null) inGameUI.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public GameObject settingsPanel;

    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
}