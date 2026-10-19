using UnityEngine;

public class MovingController
{
    private Rigidbody2D rb2d;

    private float speed = 1f;

    private Vector3 velocity;

    public MovingController(Rigidbody2D _rb2d, float _speed)
    {
        rb2d = _rb2d;
        speed = _speed;
    }

    public void SetMove(Vector2 dir) => velocity = dir * speed;

    public void Update() => rb2d.linearVelocity = velocity;
}
