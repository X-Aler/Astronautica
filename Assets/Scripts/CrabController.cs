using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class CrabController : MonoBehaviour
{
    [SerializeField] private float cooldownTime;
    [SerializeField] private float attackTime;
    private AttackHitbox hitbox;
    private PlayerHealthManager healthManager;
    private bool isAttacking = false;
    private bool cooldown = false;
    private Animator animator;
    private CinemachineCamera cam;
    private Character player;

    [SerializeField] private AudioSource grab;
    [SerializeField] private AudioSource grabEnd;
    private void Awake()
    {
        cam = FindAnyObjectByType<CinemachineCamera>();
        player = FindAnyObjectByType<Character>();
        animator = GetComponent<Animator>();
        healthManager = FindAnyObjectByType<PlayerHealthManager>();
        hitbox = GetComponent<AttackHitbox>();
        hitbox.compareTag = "Player";
    }
    private void Update()
    {
        if (hitbox.inHitBox && !isAttacking && !cooldown)
        {
            isAttacking = true;
            animator.Play("AttackApptemp");
        }
    }

    public void Grab()
    {
        if (hitbox.inHitBox)
        {
            grab.Play();
            animator.Play("AttackPerform");
            cam.Follow = transform;

            player.GetComponent<SpriteRenderer>().enabled = false;
            player.GetComponent<Character>().enabled = false;
        }

        StartCoroutine(Wait());
    }

    public void GrabEnd()
    {
        grabEnd.Play();
        healthManager.TakeDamage(100);
        isAttacking = false;
    }
    private IEnumerator Wait()
    {
        cooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isAttacking = false;
        cooldown = false;
    }
}

