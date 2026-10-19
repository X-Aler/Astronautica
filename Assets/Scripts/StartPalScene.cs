using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPalScene : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var environmentController = FindAnyObjectByType<EnvironmentController>();
        var playerHealthManager = FindAnyObjectByType<PlayerHealthManager>();
        var character = FindAnyObjectByType<Character>();

        environmentController?.StartEnvironmentController();
        playerHealthManager?.StartPlayerHealthManager();

        character.gameObject.transform.position = new Vector3(-14, 3, 0);

        AudioManager.Instance.MusicSource.Stop();
        AudioManager.Instance.MusicSourceSecond.Stop();

        AudioManager.Instance.PlayLoopingSound(AudioManager.Instance.MusicSource, AudioManager.Instance.PalPlanetTheme, AudioManager.Instance.musicVolume);
        AudioManager.Instance.PlayLoopingSound(AudioManager.Instance.MusicSourceSecond, AudioManager.Instance.Breathing, AudioManager.Instance.musicVolume);

    }

}
