using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Pusher pusher;
    private DroneAttackManager droneManager;
    private Vector2 startPos;

    private void Start()
    {
        droneManager = FindAnyObjectByType<DroneAttackManager>();
        startPos = transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, startPos) > droneManager.attackRadius)
        {
            Destroy(gameObject);
        }
    }

    public void Fly(float speed, Vector2 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealthManager enemyHealthManager = collision.GetComponent<EnemyHealthManager>();
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();

            if (enemyHealthManager != null)
            {
                enemyHealthManager.TakeDamage(droneManager.damage);
            }

            if (enemyRb != null)
            {
                Vector2 pushDir = (collision.transform.position - transform.position).normalized;
                pusher = new Pusher(enemyRb, droneManager.pushForce, droneManager.forceMode);
                pusher.Push(pushDir);
            }

            Destroy(gameObject);
        }
    }
}
