using PrimeTween;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float strength;
    [SerializeField] private int vibrato;
    [SerializeField] private float randomness;
    [SerializeField] private int fadeOut;

    private Camera cam;
    public void Start()
    {
        cam = GetComponent<Camera>();
        Shake();
    }

    private void Shake()
    {
        Tween.ShakeCamera(cam, duration, strength, vibrato, randomness, fadeOut, false)
            .OnComplete(() => Shake());
    }
}
