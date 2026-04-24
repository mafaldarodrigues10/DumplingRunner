using UnityEngine;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;

    public TextMeshProUGUI musicButtonText;
    public TextMeshProUGUI sfxButtonText;

    private bool musicOn;
    private bool sfxOn;

    void Start()
    {
        musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        sfxOn = PlayerPrefs.GetInt("SfxOn", 1) == 1;

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        ApplySettings();
    }

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

    public void ToggleMusic()
    {
        musicOn = !musicOn;
        PlayerPrefs.SetInt("MusicOn", musicOn ? 1 : 0);
        PlayerPrefs.Save();

        ApplySettings();
    }

    public void ToggleSfx()
    {
        sfxOn = !sfxOn;
        PlayerPrefs.SetInt("SfxOn", sfxOn ? 1 : 0);
        PlayerPrefs.Save();

        ApplySettings();
    }

    void ApplySettings()
    {
        if (musicButtonText != null)
            musicButtonText.text = musicOn ? "Music: ON" : "Music: OFF";

        if (sfxButtonText != null)
            sfxButtonText.text = sfxOn ? "SFX: ON" : "SFX: OFF";

        AudioListener.volume = musicOn ? 1f : 0f;
    }
}