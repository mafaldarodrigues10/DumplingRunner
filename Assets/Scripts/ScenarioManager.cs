using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScenarioManager : MonoBehaviour
{
    public Material daySky;
    public Material sunsetSky;
    public Material nightSky;

    public int sunsetPrice = 100;
    public int nightPrice = 200;

    public int sunsetScore = 200;
    public int nightScore = 300;

    private int currentScenario = -1;

    [Header("Sunset UI")]
    public Button buySunsetButton;
    public TextMeshProUGUI buySunsetText;

    [Header("Night UI")]
    public Button buyNightButton;
    public TextMeshProUGUI buyNightText;

    [Header("Menu")]
    public GameObject scenarioMenu;

    void Start()
    {
        PlayerPrefs.SetInt("DayUnlocked", 1);

        ApplyScenario(0);
        UpdateUI();
    }

    void Update()
    {
        if (ScoreManager.instance == null) return;

        int score = ScoreManager.instance.score;
        int scenarioToUse = 0;

        if (score >= nightScore && PlayerPrefs.GetInt("NightUnlocked", 0) == 1)
            scenarioToUse = 2;
        else if (score >= sunsetScore && PlayerPrefs.GetInt("SunsetUnlocked", 0) == 1)
            scenarioToUse = 1;

        if (scenarioToUse != currentScenario)
        {
            currentScenario = scenarioToUse;
            ApplyScenario(scenarioToUse);
        }
    }

    public void BuySunset()
    {
        BuyScenario("SunsetUnlocked", sunsetPrice);
    }

    public void BuyNight()
    {
        BuyScenario("NightUnlocked", nightPrice);
    }

    void BuyScenario(string unlockKey, int price)
    {
        int coins = PlayerPrefs.GetInt("TotalCoins", 0);

        if (coins >= price)
        {
            PlayerPrefs.SetInt("TotalCoins", coins - price);
            PlayerPrefs.SetInt(unlockKey, 1);
            PlayerPrefs.Save();
        }

        UpdateUI();
    }

    void ApplyScenario(int scenario)
    {
        PlayerPrefs.SetInt("CurrentRunScenario", scenario);

        if (scenario == 0 && daySky != null)
            RenderSettings.skybox = daySky;

        if (scenario == 1 && sunsetSky != null)
            RenderSettings.skybox = sunsetSky;

        if (scenario == 2 && nightSky != null)
            RenderSettings.skybox = nightSky;
    }

    void UpdateUI()
{
    bool sunsetUnlocked = PlayerPrefs.GetInt("SunsetUnlocked", 0) == 1;
    bool nightUnlocked = PlayerPrefs.GetInt("NightUnlocked", 0) == 1;

    // SUNSET
    if (buySunsetButton != null)
    {
        var text = buySunsetButton.GetComponentInChildren<TextMeshProUGUI>();

        if (sunsetUnlocked)
        {
            if (text != null) text.text = "Unlocked";
            buySunsetButton.interactable = false;
        }
        else
        {
            if (text != null) text.text = sunsetPrice + " coins";
            buySunsetButton.interactable = true;
        }
    }

    // NIGHT
    if (buyNightButton != null)
    {
        var text = buyNightButton.GetComponentInChildren<TextMeshProUGUI>();

        if (nightUnlocked)
        {
            if (text != null) text.text = "Unlocked";
            buyNightButton.interactable = false;
        }
        else
        {
            if (text != null) text.text = nightPrice + " coins";
            buyNightButton.interactable = true;
        }
    }
}

    public void CloseScenarioSelectionMenu()
    {
        if (scenarioMenu != null)
            scenarioMenu.SetActive(false);
    }

    public void ResetScenarios()
    {
        PlayerPrefs.DeleteKey("SunsetUnlocked");
        PlayerPrefs.DeleteKey("NightUnlocked");
        PlayerPrefs.DeleteKey("SelectedScenario");
        PlayerPrefs.DeleteKey("CurrentRunScenario");

        PlayerPrefs.Save();

        Debug.Log("Scenarios reset!");
    }
}