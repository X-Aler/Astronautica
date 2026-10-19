using PrimeTween;
using UnityEngine.EventSystems;

public class ButtonHandler : ButtonBase, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.buttonEnter, AudioManager.Instance.sfxVolume);
        if (transform.localScale != textManager.pointScale)
        {
            Tween.Scale(transform, textManager.pointScale, textManager.pointEnterScaleTime, textManager.pointEnterEaseStyle);
        }
    }
    public void OnPointerExit(PointerEventData eventData) => ResetChanges();
    public void OnPointerDown(PointerEventData eventData) 
    {
        if (txt.color != textManager.clickColor) 
        { 
            Tween.Color(txt, textManager.clickColor, textManager.clickColorScaleTime, textManager.clickColorEaseStyle); 
        }
    }
}
