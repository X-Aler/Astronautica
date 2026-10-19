using System;
using System.Collections;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private float attackRadius;

    [SerializeField] private float attackCooldown;

    [SerializeField] private float pushForce;

    [SerializeField] private ForceMode2D forceMode;

    public event Action onAttackPerform;
    public event Action onAttackEnd;
    private Pusher pusher;
    private Character player;
    private PlayerHealthManager healthManager;
    private AttackHitbox attackHitbox;

    public bool isAttacking;
    private bool onCooldown;

    private void Awake()
    {
        attackHitbox = GetComponent<AttackHitbox>();
    }

    private void Start()
    {
        player = FindAnyObjectByType<Character>();
        pusher = new Pusher(player.GetComponent<Rigidbody2D>(), pushForce, forceMode);
        healthManager = player.GetComponent<PlayerHealthManager>();
    }

    private void Update()
    {
        if (attackHitbox.inHitBox)
        {
            StartAttack();
        }
    }
    public void DealDamage()
    {
        if (attackHitbox.inHitBox || Vector2.Distance(transform.position, player.transform.position) < attackRadius)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            pusher.Push(direction);
            healthManager.TakeDamage(damage);
        }
    }

    public void StartAttack()
    {
        if (isAttacking || onCooldown) return;

        isAttacking = true;
        onAttackPerform?.Invoke();
    } 

    public void EndAttack()
    {
        onCooldown = true;
        isAttacking = false;
        onAttackEnd?.Invoke();
        StartCoroutine(Cooldown());
    }

    public float GetAttackRadius() => attackRadius;

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        onCooldown = false;
    }
}
