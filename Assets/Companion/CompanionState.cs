using UnityEngine;

public abstract class CompanionState
{
    protected CompanionAI ai;

    public CompanionState(CompanionAI ai)
    {
        this.ai = ai;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
