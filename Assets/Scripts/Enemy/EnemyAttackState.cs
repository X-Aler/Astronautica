using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy) : base(enemy) {}

    public override void Update()
    {
        enemy.StopMove();
        enemy.attackAnimator.SetAttack(enemy.GetAttackDirection());

        if (Vector2.Distance(enemy.transform.position, enemy.player.transform.position) < enemy.AttackManager.GetAttackRadius())
        {
            enemy.attackAnimator.StartAttack();
        }
        else
        {
            enemy.SetState(enemy.walkState);
        }
    }

    public override void Idle()
    {
        //??????
    }


    public override void Walk()
    {
        //??????
    }
}
