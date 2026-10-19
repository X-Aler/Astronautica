using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Toggle Vsync;

    private void Start()
    {
        musicSlider.value = AudioManager.Instance.musicVolume;
        SFXSlider.value = AudioManager.Instance.sfxVolume;
        Vsync.isOn = QualitySettings.vSyncCount > 0;

        AudioManager.Instance.SetMusicVolume(musicSlider.value);
        AudioManager.Instance.SetSFXVolume(SFXSlider.value);
    }

    public void OnMusicSliderChange()
    {
        AudioManager.Instance.SetMusicVolume(musicSlider.value);
    }

    public void OnSFXSliderChange()
    {
        AudioManager.Instance.SetSFXVolume(SFXSlider.value);
    }

    public void OnVsyncChange()
    {
        QualitySettings.vSyncCount = Vsync.isOn ? 1 : 0;
    }
}