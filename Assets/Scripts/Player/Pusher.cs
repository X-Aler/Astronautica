using UnityEngine;

public class Pusher
{
    private float pushForce;
    private Rigidbody2D rb;
    ForceMode2D forceMode;

    public Pusher(Rigidbody2D rb, float pushForce, ForceMode2D forceMode)
    {
        this.rb = rb;
        this.pushForce = pushForce;
    }

    public void Push(Vector2 dir)
    {
        rb.AddForce(dir * pushForce, forceMode);
    }
}
