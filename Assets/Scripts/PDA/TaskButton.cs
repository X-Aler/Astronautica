using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class TaskButton : ButtonBase, IPointerUpHandler
{
    [SerializeField] private TextData taskData;
    [SerializeField] private TextData endData;
    [SerializeField] private float delayToPrint;
    [SerializeField] private CoroutinePerformer performer;
    [SerializeField] private TextMeshProUGUI text;

    private TextWriter writer;

    private bool open = false;

    private void Start()
    {
        writer = new TextWriter(performer, text, CosmicLift.liftFounded ? endData : taskData, delayToPrint, true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.PDAButtonClick, AudioManager.Instance.sfxVolume);
        
        open = !open;

        if (open)
        {
            writer.PlayText();
        }
        else
        {
            AudioManager.Instance.SFXSource.Stop();
        }

        text.transform.parent.gameObject.SetActive(open);
    }
}
