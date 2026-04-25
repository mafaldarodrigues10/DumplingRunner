using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider musicSlider;
    public Toggle sfxToggle;

    void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        bool sfxOn = PlayerPrefs.GetInt("SfxOn", 1) == 1;

        if (musicSlider != null)
        {
            musicSlider.value = musicVolume;
            musicSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.AddListener(OnMusicChanged);
        }

        if (sfxToggle != null)
        {
            sfxToggle.isOn = sfxOn;
            sfxToggle.onValueChanged.RemoveAllListeners();
            sfxToggle.onValueChanged.AddListener(OnSfxChanged);
        }

        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetMusicVolume(musicVolume);
            AudioManager.instance.SetSfxEnabled(sfxOn);
        }
    }

    public void OnMusicChanged(float value)
    {
        if (AudioManager.instance != null)
            AudioManager.instance.SetMusicVolume(value);
    }

    public void OnSfxChanged(bool isOn)
    {
        if (AudioManager.instance != null)
            AudioManager.instance.SetSfxEnabled(isOn);
    }
}