using UnityEngine;

public class AttackAnimate
{
    private Animator animator;

    private string valueName;

    private string nameX;

    private string nameY;

    public AttackAnimate(Animator animator, string valueName, string nameX, string nameY)
    {
        this.animator = animator;
        this.valueName = valueName;
        this.nameX = nameX;
        this.nameY = nameY;
    }

    public void StartAttack()
    {
        animator.SetBool(valueName, true);
    }

    public void SetAttack(Vector2 direction)
    {
        animator.SetFloat(nameX, direction.x);
        animator.SetFloat(nameY, direction.y);
    }

    public void EndAttack()
    {
        animator.SetBool(valueName, false);
    }
}
