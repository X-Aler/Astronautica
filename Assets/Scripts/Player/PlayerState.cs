public abstract class PlayerState
{
    protected Character character;
    public PlayerState(Character character)
    {
        this.character = character;
    }

    public abstract void Update();
    public abstract void Idle();
    public abstract void Walk();
}
