using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music")]
    public AudioClip backgroundMusic;

    [Header("SFX Clips")]
    public AudioClip dotCollectSound;
    public AudioClip speedBoostSound;
    public AudioClip shootPickupSound;
    public AudioClip projectileFireSound;
    public AudioClip enemyCatchSound;
    public AudioClip levelCompleteSound;
    public AudioClip buttonClickSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayDotCollect()
    {
        PlaySFX(dotCollectSound);
    }

    public void PlaySpeedBoost()
    {
        PlaySFX(speedBoostSound);
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.ShowPowerUpText(
                "SPEED BOOST!", 2f);
    }

    public void PlayShootPickup()
    {
        PlaySFX(shootPickupSound);
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.ShowPowerUpText(
                "SHOOTING UNLOCKED! Press Space", 3f);
    }

    public void PlayProjectileFire()
    {
        PlaySFX(projectileFireSound);
    }

    public void PlayEnemyCatch()
    {
        PlaySFX(enemyCatchSound);
    }

    public void PlayLevelComplete()
    {
        PlaySFX(levelCompleteSound);
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClickSound);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
        else
            Debug.LogWarning("Audio clip not assigned!");
    }
}