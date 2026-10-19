using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ExitButton : ButtonBase, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.buttonClick, AudioManager.Instance.sfxVolume);
        ResetChanges();
        Application.Quit();
    }
}
