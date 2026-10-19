using PrimeTween;
using System.Collections;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer groupOne;
    [SerializeField] private SpriteRenderer groupTwo;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float CooldownBetweenFrames = 0.5f;
    [SerializeField] private float transistDuration = 0.5f;

    private int current = 0;
    private int next => (current + 1) % sprites.Length;

    private void Awake()
    {
        StartCoroutine(changeSprite());
    }

    private void Start()
    {
        AudioManager.Instance.MusicSource.Stop();
        AudioManager.Instance.MusicSourceSecond.Stop();
        AudioManager.Instance.PlayLoopingSound(AudioManager.Instance.MusicSource, AudioManager.Instance.mainMenuTheme, AudioManager.Instance.musicVolume);
    }
    private IEnumerator changeSprite()
    {
        groupOne.sprite = sprites[current];
        groupTwo.sprite = sprites[next];

        while (true)
        {
            Tween.Alpha(groupOne, 0, transistDuration);
            Tween.Alpha(groupTwo, 1, transistDuration);

            yield return new WaitForSeconds(transistDuration + CooldownBetweenFrames);

            current = next;
            groupOne.sprite = sprites[current];
            groupOne.color = new Color(1, 1, 1, 1);

            groupTwo.sprite = sprites[(current + 1) % sprites.Length];
            groupTwo.color = new Color(1, 1, 1, 0);
        }
    }
}
