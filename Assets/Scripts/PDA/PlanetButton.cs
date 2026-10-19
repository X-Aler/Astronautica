using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetButton : ButtonBase, IPointerUpHandler
{
    [SerializeField] private GameObject screen;
    private bool open = false;

    private void Start()
    {
        screen.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.PDAButtonClick, AudioManager.Instance.sfxVolume);
        open = !open;
        screen.SetActive(open);
    }
}
