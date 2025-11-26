using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Start Stagger Cooldown", story: "Stagger Cooldown Start", category: "Action", id: "a21578324ec70f5c261efcdd924bf2e2")]
public partial class StartStaggerCooldownAction : Action
{
    [SerializeReference] public BlackboardVariable<Boolean> Stagger;
    [SerializeReference] public BlackboardVariable<float> Duration;

    private float t;
    protected override Status OnStart()
    {
        t = Duration.Value;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        t -= Time.deltaTime;
        while (t > 0)
        {
            return Status.Running;
        }
        Stagger.Value = false;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

