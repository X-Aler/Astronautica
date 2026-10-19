using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathContoller : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private Image sceen;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextData data;
    [SerializeField] private TextData Overdata;
    [SerializeField] private float delayToSceren;
    [SerializeField] private float delayToText;
    [SerializeField] private CoroutinePerformer performer;
    [SerializeField] private float startAlpha;
    [SerializeField] private float endAlpha;
    public static bool isOver = false;

    private TextWriter textWriter;

    private void Awake()
    {
        button.SetActive(false);
        textWriter = new TextWriter(performer, text, isOver ? Overdata : data, delayToText, true);

        if (!isOver)
        {
            CosmicLift.liftFounded = false;
        }

    }
    void Start()
    {
        Tween.Alpha(sceen, startAlpha, endAlpha, delayToSceren).OnComplete(() => button.SetActive(true));
        textWriter.PlayDeathText();
    }

}
