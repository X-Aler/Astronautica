using PrimeTween;
using UnityEngine;

public class VoidsManager
{
    private SpriteRenderer currentVoid;

    private float timeToChange;

    private bool isDay;

    private bool inverse;

    public VoidsManager(SpriteRenderer currentVoid, float timeToChange, bool isDay, bool inverse)
    {
        this.currentVoid = currentVoid;
        this.timeToChange = timeToChange;
        this.isDay = isDay;
        this.inverse = inverse;
    }

    public void SetVoids()
    {
        float Alpha = isDay ? 1 : 0;
        if (inverse) Alpha = 1 - Alpha;
        SetVoidsAlpha(currentVoid, Alpha);
    }

    private void SetVoidsAlpha(SpriteRenderer sprite, float alpha)
    {
        if (Mathf.Approximately(sprite.color.a, alpha)) return;
        Tween.Alpha(sprite, alpha, timeToChange);
    }
}
