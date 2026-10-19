using System.Collections;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    private Animator planetAnimation;
    public static string currentPlanet;

    private void Start()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.CosmisShipFly, AudioManager.Instance.sfxVolume);
        planetAnimation = GetComponent<Animator>();
        StartCoroutine(FlyCoroutine());
    }

    public static void SelectPlanet(string planetName)
    {
        currentPlanet = planetName;
        Debug.Log("PlanetManager received: " + planetName);
        Debug.Log("currentPlanet is now: " + currentPlanet);
    }

    private IEnumerator FlyCoroutine()
    {
        planetAnimation.Play(currentPlanet);
        if (currentPlanet == "DEATH")
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.manuFlyTheme, AudioManager.Instance.sfxVolume);
            DeathContoller.isOver = true;
        }
        else
        {
            DeathContoller.isOver = false;
        }
        yield return new WaitForSeconds(3.2f);
        LoadingManager.LoadScene(currentPlanet);
        currentPlanet = null;
    }
}