using PrimeTween;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PDAController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer planetToHide;
    [SerializeField] private GameObject PDA;
    [SerializeField] private GameObject text;
    [SerializeField] private float duration;
    [SerializeField] private Ease easeStyle;
    [SerializeField] private Vector3 startPos;

    Tween openTween;
    private bool open = false;
    private bool canOpen = true;

    public static bool isCardFound = false;

    private void Awake()
    {
        PDA.SetActive(false);
        PDA.transform.localPosition = startPos;
    }

    private void Start()
    {
        AudioManager.Instance.MusicSource.Stop();
        AudioManager.Instance.MusicSourceSecond.Stop();
        AudioManager.Instance.PlayLoopingSound(AudioManager.Instance.MusicSource, AudioManager.Instance.CosmisShip, AudioManager.Instance.musicVolume);
        AudioManager.Instance.PlayLoopingSound(AudioManager.Instance.MusicSourceSecond, AudioManager.Instance.CosmisShipLight, AudioManager.Instance.musicVolume);
    }

    public void InteractPDA(InputAction.CallbackContext context)
    {
        if (!canOpen) return;
        if (PDA == null) return;
        AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.PDAInteract, AudioManager.Instance.sfxVolume);
        open = !open;
        canOpen = false;
        if (!openTween.IsUnityNull())
        {
            openTween.Stop();
        }

        if (open)
        {
            if (PDA.transform.localPosition != Vector3.zero)
            {
                PDA.SetActive(true);
                text.SetActive(false);
                openTween = Tween.LocalPosition(PDA.transform, Vector3.zero, duration, easeStyle)
                    .OnComplete(() =>
                    {
                        canOpen = true;
                    }); ;
            }
            else
            {
                PDA.SetActive(true);
                canOpen = true;
            }
        }
        else
        {
            if (PDA.transform.localPosition != startPos)
            {
                openTween = Tween.LocalPosition(PDA.transform, startPos, duration, easeStyle)
                .OnComplete(() =>
                {
                    PDA.SetActive(false);
                    text.SetActive(true);
                    canOpen = true;
                });
            }
            else
            {
                PDA.SetActive(false);
                canOpen = true;
            }
        }
    }
}
