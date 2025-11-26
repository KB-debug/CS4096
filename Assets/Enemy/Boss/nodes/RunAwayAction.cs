using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Run Away", story: "[Agent] Runs", category: "Action", id: "55cfdf6c0c7ed599e39c7d7eeb5ea70a")]
public partial class RunAwayAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

