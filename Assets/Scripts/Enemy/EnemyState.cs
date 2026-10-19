public abstract class EnemyState
{
    protected Enemy enemy;
    public EnemyState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public abstract void Update();
    public abstract void Idle();
    public abstract void Walk();
}
