using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CosmicLift : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private TextData data;
    [SerializeField] private TextData missionsData;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CoroutinePerformer performer;
    [SerializeField] private float delay;
    TextWriter writer;
    private bool inZone;

    public static bool liftFounded = false;

    private void Awake()
    {
        writer = new TextWriter(performer, text, data, delay, false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (liftFounded) return;
            inZone = true;
            button.SetActive(inZone);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inZone = false;
            button.SetActive(inZone);
        }
    }

    public void PickUp(InputAction.CallbackContext context)
    {
        if (inZone && context.performed && !liftFounded)
        {
            liftFounded = true;
            button.SetActive(false);
            writer.PlayText();
        }
    }
}
