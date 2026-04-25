using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip coinSound;
    public AudioClip hitSound;
    public AudioClip buttonClickSound;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();

        if (musicSource != null)
            musicSource.volume = value;
    }

    public void SetMusicEnabled(bool enabled)
    {
        SetMusicVolume(enabled ? 1f : 0f);
    }

    public void SetSfxEnabled(bool enabled)
    {
        PlayerPrefs.SetInt("SfxOn", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void PlayCoin()
    {
        PlaySfx(coinSound);
    }

    public void PlayHit()
    {
        PlaySfx(hitSound);
    }

    public void PlayButtonClick()
    {
        PlaySfx(buttonClickSound);
    }

    void PlaySfx(AudioClip clip)
    {
        if (PlayerPrefs.GetInt("SfxOn", 1) == 0) return;
        if (sfxSource == null || clip == null) return;

        sfxSource.PlayOneShot(clip);
    }
}