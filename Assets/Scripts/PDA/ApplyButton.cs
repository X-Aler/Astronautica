using UnityEngine;
using UnityEngine.EventSystems;
public class ApplyButton : ButtonBase, IPointerUpHandler
{ 
    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.PDAButtonClick, AudioManager.Instance.sfxVolume);
        if (!string.IsNullOrEmpty(PlanetManager.currentPlanet))
        {
            LoadingManager.LoadScene("FlyToPlanetScene");
        }
        else
        {
            Debug.Log("Selected Planet: " + PlanetManager.currentPlanet);
        }
    }
}
