using UnityEngine;

public class Rotator : MonoBehaviour
{
    public void SetRotate(Vector2 direction, float speed)
    {
        if (transform == null) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    public Quaternion GetRotation(Vector2 dir)
    {
        if (dir == Vector2.zero) return Quaternion.identity;
        float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot);
    }
}
