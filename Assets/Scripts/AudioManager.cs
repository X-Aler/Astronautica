using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioMixer mixer;

    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    public AudioSource SFXSource;
    public AudioSource PlayerSFX;
    public AudioSource MusicSource;
    public AudioSource MusicSourceSecond;

    public AudioClip TakeDamage;
    public AudioClip Walk;

    public AudioClip mainMenuTheme;
    public AudioClip buttonClick;
    public AudioClip buttonEnter;

    public AudioClip TextTyping;
    public AudioClip DeathTextTyping;

    public AudioClip PDAButtonClick;
    public AudioClip PDAInteract;
    public AudioClip PlanetChoose;
    public AudioClip CosmisShip;
    public AudioClip CosmisShipLight;
    public AudioClip CosmisShipFly;
    public AudioClip PalPlanetTheme;
    public AudioClip Breathing;

    public AudioClip manuFlyTheme;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        float db = Mathf.Log10(Mathf.Clamp(musicVolume, 0.0001f, 1f)) * 20f;
        mixer.SetFloat("MusicVolume", db);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        float db = Mathf.Log10(Mathf.Clamp(sfxVolume, 0.0001f, 1f)) * 20f;
        mixer.SetFloat("SFXVolume", db);
    }

    public void PlaySound(AudioSource sound, AudioClip clip, float volume)
    {
        if (clip != null && sound != null)
        {
            sound.PlayOneShot(clip, volume);
        }
    }

    public void PlayLoopingSound(AudioSource source, AudioClip clip, float volume)
    {
        if (clip != null && source != null)
        {
            source.clip = clip;
            source.volume = volume;
            source.loop = true;
            source.Play();
        }
    }

    public void StopLoopingSound(AudioSource source)
    {
        if (source != null)
        {
            source.loop = false;
            source.Stop();
        }
    }
}
