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

    public Button buySunsetButton;
    public Button selectSunsetButton;
    public TextMeshProUGUI buySunsetText;

    public Button buyNightButton;
    public Button selectNightButton;
    public TextMeshProUGUI buyNightText;

    void Start()
    {
        PlayerPrefs.SetInt("DayUnlocked", 1);
        UpdateUI();
        ApplySelectedSky();
    }

    public void SelectDay()
    {
        PlayerPrefs.SetInt("SelectedScenario", 0);
        PlayerPrefs.Save();
        ApplySelectedSky();
        UpdateUI();
    }

    public void BuySunset()
    {
        BuyScenario("SunsetUnlocked", sunsetPrice);
    }

    public void SelectSunset()
    {
        if (PlayerPrefs.GetInt("SunsetUnlocked", 0) == 1)
        {
            PlayerPrefs.SetInt("SelectedScenario", 1);
            PlayerPrefs.Save();
            ApplySelectedSky();
            UpdateUI();
        }
    }

    public void BuyNight()
    {
        BuyScenario("NightUnlocked", nightPrice);
    }

    public void SelectNight()
    {
        if (PlayerPrefs.GetInt("NightUnlocked", 0) == 1)
        {
            PlayerPrefs.SetInt("SelectedScenario", 2);
            PlayerPrefs.Save();
            ApplySelectedSky();
            UpdateUI();
        }
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

    void ApplySelectedSky()
    {
        int selected = PlayerPrefs.GetInt("SelectedScenario", 0);

        if (selected == 0 && daySky != null) RenderSettings.skybox = daySky;
        if (selected == 1 && sunsetSky != null) RenderSettings.skybox = sunsetSky;
        if (selected == 2 && nightSky != null) RenderSettings.skybox = nightSky;
    }

    void UpdateUI()
    {
        int selected = PlayerPrefs.GetInt("SelectedScenario", 0);

        bool sunsetUnlocked = PlayerPrefs.GetInt("SunsetUnlocked", 0) == 1;
        bool nightUnlocked = PlayerPrefs.GetInt("NightUnlocked", 0) == 1;

        if (buySunsetButton != null) buySunsetButton.gameObject.SetActive(!sunsetUnlocked);
        if (selectSunsetButton != null) selectSunsetButton.gameObject.SetActive(sunsetUnlocked);
        if (buySunsetText != null) buySunsetText.text = sunsetPrice + " coins";

        if (buyNightButton != null) buyNightButton.gameObject.SetActive(!nightUnlocked);
        if (selectNightButton != null) selectNightButton.gameObject.SetActive(nightUnlocked);
        if (buyNightText != null) buyNightText.text = nightPrice + " coins";

        if (selectSunsetButton != null)
            selectSunsetButton.GetComponentInChildren<TextMeshProUGUI>().text = selected == 1 ? "Selected" : "Select";

        if (selectNightButton != null)
            selectNightButton.GetComponentInChildren<TextMeshProUGUI>().text = selected == 2 ? "Selected" : "Select";
    }
}