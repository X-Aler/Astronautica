using System.Collections;
using TMPro;
using UnityEngine;

public class TextWriter
{
    private TextMeshProUGUI writeText;
    private TextData fullText;
    private float delay;
    private bool needDot;
    private CoroutinePerformer performer;

    private Coroutine coroutine;
    public TextWriter(CoroutinePerformer performer, TextMeshProUGUI writeText, TextData fullText, float Delay, bool needDot)
    {
        this.performer = performer;
        this.writeText = writeText;
        this.fullText = fullText;
        this.delay = Delay;
        this.needDot = needDot;
    }

    public void PlayText()
    {
        if (coroutine != null)
        {
            performer.StopAllCoroutines();
        }

        coroutine = performer.StartCoroutine(WriteText());
    }

    public void PlayDeathText()
    {
        if (coroutine != null)
        {
            performer.StopAllCoroutines();
        }

        coroutine = performer.StartCoroutine(WriteDeathText());
    }

    private IEnumerator WriteText()
    {
        writeText.text = "";
        AudioManager.Instance.PlayLoopingSound(AudioManager.Instance.SFXSource, AudioManager.Instance.TextTyping, AudioManager.Instance.sfxVolume);
        foreach (char c in fullText.text)
        {
            writeText.text += c;
            yield return new WaitForSeconds(delay);
        }
        AudioManager.Instance.SFXSource.Stop();
        if (!needDot) yield return null;
        performer.StartCoroutine(DotAtEnd());
    }

    private IEnumerator WriteDeathText()
    {
        writeText.text = "";
        foreach (char c in fullText.text)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.DeathTextTyping, AudioManager.Instance.sfxVolume);
            writeText.text += c;
            yield return new WaitForSeconds(delay);
        }
        AudioManager.Instance.SFXSource.Stop();
        performer.StartCoroutine(DotAtEnd());
    }

    private IEnumerator DotAtEnd()
    {
        bool visible = false;

        while (true)
        {
            if (visible)
            {
                writeText.text = writeText.text.TrimEnd('.');
            }
            else
            {
                writeText.text += '.';
            }
            visible = !visible;
            yield return new WaitForSeconds(1f);
        }
    }
}
