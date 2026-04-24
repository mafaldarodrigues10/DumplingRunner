using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Transform player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;

    public int score = 0;
    public int runCoins = 0;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameOver)
            return;

        score = Mathf.FloorToInt(player.position.z);

        scoreText.text = "Score: " + score;
        coinText.text = "Coins: " + runCoins;
    }

    public void AddCoin(int amount)
    {
        runCoins += amount;
    }
}