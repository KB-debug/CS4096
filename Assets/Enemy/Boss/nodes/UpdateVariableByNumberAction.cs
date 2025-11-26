using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Update Variable By Number", story: "[Variable] Change by [ValueIncrease]", category: "Action", id: "aff62635defe314b700ad2dce2d7ce6a")]
public partial class UpdateVariableByNumberAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Variable;
    [SerializeReference] public BlackboardVariable<int> ValueIncrease;

    protected override Status OnStart()
    {
        Variable.Value += ValueIncrease.Value;
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

