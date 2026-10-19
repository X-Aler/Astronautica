using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttackManager : MonoBehaviour
{
    public float damage;
    public float attackRadius;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float flySpeed;
    [SerializeField] private float rotateSpeed;
    public float pushForce;
    [SerializeField] private GameObject bulletPrefab;
    public ForceMode2D forceMode;
    DroneController droneController;
    public AttackHitbox hitbox;
    public Transform closest;
    public bool isAttacking;
    public bool onCooldown;
    public Vector2 direction;

    private void Awake()
    {
        hitbox = GetComponent<AttackHitbox>();
        droneController = GetComponent<DroneController>();
    }

    private void Update()
    {

        if (hitbox.inHitBox)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (isAttacking || onCooldown || hitbox.targets.Count == 0) return;

        isAttacking = true;
        closest = ReturnClosest(hitbox.targets);

        if (closest != null)
        {
            StartCoroutine(Attacking(closest));
        }
        else
        {
            isAttacking = false;
        }
    }

    private IEnumerator Attacking(Transform closest)
    {
        if (closest == null)
        {
            isAttacking = false;
            yield break;
        }

        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.SetParent(null);

        direction = (closest.position - transform.position).normalized;
        droneController.rotator.SetRotate(direction, rotateSpeed);
        bullet.GetComponent<BulletController>().Fly(flySpeed, direction);

        onCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        onCooldown = false;
        isAttacking = false;
    }

    private Transform ReturnClosest(List<GameObject> targets)
    {
        float closestDistance = float.MaxValue;
        Transform closestTarget = null;

        foreach (var target in targets)
        {
            if (target == null) continue;

            float targetDistance = Vector2.Distance(transform.position, target.transform.position);

            if (targetDistance < closestDistance)
            {
                closestDistance = targetDistance;
                closestTarget = target.transform;
            }
        }
        return closestTarget;
    }
}