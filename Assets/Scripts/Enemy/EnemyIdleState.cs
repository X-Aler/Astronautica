using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy) : base(enemy) { }

    public override void Update()
    {
        if (Vector2.Distance(enemy.transform.position, enemy.player.transform.position) > enemy.AttackManager.GetAttackRadius())
        {
            enemy.SetState(enemy.walkState);
        }
        else if (enemy.AttackManager.isAttacking)
        {
            enemy.SetState(enemy.attackState);
        }
        else
        {
            enemy.StopMove();
        }
    }
 

    public override void Idle()
    {
        enemy.enemyData.render.sprite = enemy.enemyData.GetSprite(EnemyStateType.Idle, DirectionType.Front);
    }

    public override void Walk()
    {
        //������
    }
}
