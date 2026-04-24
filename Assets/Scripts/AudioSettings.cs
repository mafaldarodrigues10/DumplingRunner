using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider musicSlider;
    public Toggle sfxToggle;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        bool sfxOn = PlayerPrefs.GetInt("SfxOn", 1) == 1;

        musicSlider.value = volume;
        sfxToggle.isOn = sfxOn;

        SetVolume(volume);
        ApplySfx(sfxOn);
    }

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();

        AudioListener.volume = value;
    }

    public void ApplySfx(bool isOn)
    {
        PlayerPrefs.SetInt("SfxOn", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}