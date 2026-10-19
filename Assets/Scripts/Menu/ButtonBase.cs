using PrimeTween;
using TMPro;
using UnityEngine;

public class ButtonBase : MonoBehaviour
{

    protected TextManager textManager;
    protected TextMeshProUGUI txt;

    protected virtual void Awake()
    {

        txt = GetComponentInChildren<TextMeshProUGUI>();
        textManager = FindAnyObjectByType<TextManager>();
    }

    protected void ResetChanges()
    {
        if (transform.localScale != textManager.baseScale)
        {
            Tween.Scale(transform, textManager.baseScale, textManager.pointExitScaleTime, textManager.pointExitEaseStyle);
        }

        if (txt.color != textManager.baseColor)
        {
            Tween.Color(txt, textManager.baseColor, textManager.clickColorScaleTime, textManager.clickColorEaseStyle);
        }
    }
}