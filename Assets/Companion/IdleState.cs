using UnityEngine;

public class IdleState : CompanionState
{
    private Vector3 wanderTarget;

    public IdleState(CompanionAI ai) : base(ai) { }

    public override void Enter()
    {
        ai.idleOrigin = ai.player.position;
        PickNewWanderTarget();
    }

    public override void Update()
    {
        ai.Wander(wanderTarget);

        
        float dist = Vector3.Distance(ai.player.position, ai.idleOrigin);
        if (dist > ai.idleBreakDistance)
            ai.SwitchState(ai.followState);

        
        if (Vector3.Distance(ai.transform.position, wanderTarget) < 1f)
            PickNewWanderTarget();
    }

    public override void Exit() { }

    private void PickNewWanderTarget()
    {
        float radius = ai.idleWanderRadius;
        Vector3 newTarget;

        do
        {
            Vector2 circle = Random.insideUnitCircle * radius;
            newTarget = ai.idleOrigin + new Vector3(circle.x, 0, circle.y);
        }
        while (Vector3.Distance(newTarget, ai.player.position) < ai.idleMinDistanceFromPlayer);

        wanderTarget = newTarget;
        ai.agent.SetDestination(wanderTarget);
    }

}
