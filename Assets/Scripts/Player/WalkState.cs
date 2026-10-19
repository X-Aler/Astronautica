using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(Character character) : base(character) { }

    public override void Update()
    {
        character.Animator.AnimateDirection(character.GetMoveDirection());
    }

    public override void Idle()
    {
        //ничего
    }

    public override void Walk()
    {
        Vector2 move = character.GetMoveDirection();

        if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
        {
            character.playerData.characterRenderer.sprite = character.playerData.GetSprite(
                PlayerStateType.Walk,
                move.x > 0 ? DirectionType.Right : DirectionType.Left
            );
        }
        else if (move.y != 0)
        {
            character.playerData.characterRenderer.sprite = character.playerData.GetSprite(
                PlayerStateType.Walk,
                move.y > 0 ? DirectionType.Back : DirectionType.Front
            );
        }
    }
}
