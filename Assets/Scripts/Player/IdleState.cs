using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Character character) : base(character) {}

    public  override void Update()
    {
        character.Animator.AnimateDirection(Vector2.zero);
    }
    public override void Idle()
    {
        character.playerData.characterRenderer.sprite = character.playerData.GetSprite(PlayerStateType.Idle, DirectionType.Front);
    }

    public override void Walk()
    {
        //ничего
    }
}
