using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CosmicShipController : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject staticShip;
    [SerializeField] private GameObject dynamicShip;
    [SerializeField] private GameObject text;
    CinemachineCamera cam;
    private bool inZone = false;
    private Collider2D target;

    private void Awake()
    {
        cam = FindAnyObjectByType<CinemachineCamera>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision;
            inZone = true;
            if (!CosmicLift.liftFounded) return;
            button.SetActive(inZone);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = null;
            inZone = false;
            button.SetActive(inZone);
        }
    }

    public void Interacted(InputAction.CallbackContext context)
    {
        if (context.performed && inZone && CosmicLift.liftFounded)
        {
            if (target != null)
            {
                text.SetActive(false);
                button.SetActive(false);
                target.GetComponent<SpriteRenderer>().enabled = false;
                staticShip.SetActive(false);
                dynamicShip.SetActive(true);
                cam.Follow = dynamicShip.transform;
                cam.LookAt = dynamicShip.transform;
                cam.transform.SetParent(dynamicShip.transform);
                StartCoroutine(wait());
            }
        }
    }

    private IEnumerator wait()
    {
        dynamicShip.GetComponent<Animator>().Play("ShipFly");
        AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.CosmisShipFly, AudioManager.Instance.sfxVolume);
        yield return new WaitForSeconds(1.20f);
        LoadingManager.LoadScene("PDAScene");
    }
}

