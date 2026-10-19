using UnityEngine.EventSystems;

public class PlayButton : ButtonBase, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.buttonClick, AudioManager.Instance.sfxVolume);
        ResetChanges();
        LoadingManager.LoadScene("PDAScene");
    }

}
