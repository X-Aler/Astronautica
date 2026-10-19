using UnityEngine.EventSystems;

public class DenyButton : ButtonBase, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.PDAButtonClick, AudioManager.Instance.sfxVolume);
        if (PlanetHandler.SelectedPlanet == null) return;

        PlanetHandler.SelectedPlanet.animator.SetBool("atPoint", false);
        PlanetHandler.SelectedPlanet.isSelected = false;
        PlanetHandler.SelectedPlanet.outLine.SetActive(false);

        PlanetHandler.SelectedPlanet = null;
        PlanetManager.SelectPlanet(null);
    }
}
