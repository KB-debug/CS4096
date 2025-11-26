using UnityEngine;

public class FollowState : CompanionState
{
    public FollowState(CompanionAI ai) : base(ai) { }

    public override void Enter() { }

    public override void Update()
    {
        ai.FollowPlayerLogic();

        
        if (ai.PlayerStoppedLongEnough())
            ai.SwitchState(ai.idleState);
    }

    public override void Exit() { }
}
