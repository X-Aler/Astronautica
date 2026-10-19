using UnityEngine;

public class EnemyWalkState : EnemyState
{
    public EnemyWalkState(Enemy enemy) : base(enemy) { }

    public override void Update()
    {
        float distance = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        if (distance > enemy.AttackManager.GetAttackRadius())
        {
            enemy.MoveToPlayer();
        }
        else
        {
            enemy.StopMove();
            enemy.SetState(enemy.attackState);
        }
    }

    public override void Walk()
    {
        Vector2 move = enemy.GetMoveDirection();

        if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
        {
            enemy.enemyData.render.sprite = enemy.enemyData.GetSprite(EnemyStateType.Walk, move.x > 0 ? DirectionType.Right : DirectionType.Left);
        }
        else
        {
            enemy.enemyData.render.sprite = enemy.enemyData.GetSprite(EnemyStateType.Walk, move.y > 0 ? DirectionType.Back : DirectionType.Front);
        }

    }

    public override void Idle()
    {
        //������
    }
}
