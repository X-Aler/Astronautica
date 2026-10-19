using UnityEngine;

public class MoveAnimate
{
    private Animator animator;

    private string nameX;
    private string nameY;

    public MoveAnimate(Animator _animator, string _nameX, string _nameY)
    {
        animator = _animator;
        nameX = _nameX;
        nameY = _nameY; 
    }

    public void AnimateDirection(Vector2 direction)
    {
        animator.SetFloat(nameX, direction.x);
        animator.SetFloat(nameY, direction.y);
    }
}
