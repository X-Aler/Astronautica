using PrimeTween;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public Vector3 pointScale;
    public float pointEnterScaleTime;
    public float pointExitScaleTime;
    public Vector3 baseScale;

    public Ease pointEnterEaseStyle;
    public Ease pointExitEaseStyle;

    public Vector3 clickScale;
    public float clickEnterScaleTime;

    public float clickColorScaleTime;
    public Color baseColor;
    public Color clickColor;

    public Ease clickColorEaseStyle;

    public Ease clickEaseStyle;
}
