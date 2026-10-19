using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlanetCard : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private TextData data;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CoroutinePerformer performer;
    [SerializeField] private float delay;
    TextWriter writer;
    private bool inZone;

    private void Awake()
    {
        writer = new TextWriter(performer, text, data, delay, false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PDAController.isCardFound) return;
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
        if (inZone && context.performed && !PDAController.isCardFound)
        {
            button.SetActive(false);
            PDAController.isCardFound = true;
            writer.PlayText();
            StartCoroutine(wait());
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(5f);
        text.gameObject.SetActive(false);
    }
}
