using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Change Agent Stats", story: "Agent Stats Change", category: "Action", id: "0b07d6095db33bf555b66b22830052e2")]
public partial class ChangeAgentStatsAction : Action
{

    [SerializeReference] public BlackboardVariable<float> BossHealth;
    [SerializeReference] public BlackboardVariable<float> PlayerHealth;
    [SerializeReference] public BlackboardVariable<float> AttackSpeed;
    [SerializeReference] public BlackboardVariable<int> MaxJumpCount;


    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (BossHealth.Value > 50) {
            AttackSpeed.Value = 1f;
        } else if (BossHealth < 50)
        {
            AttackSpeed.Value = 0.3f;
            MaxJumpCount.Value = 5;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

