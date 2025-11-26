using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Add Force Local", story: "Apply Force [x] to [Agent]", category: "Action", id: "6e34c25484c9ca222be975915f772d74")]
public partial class AddForceLocalAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> X;
    [SerializeReference] public BlackboardVariable<Rigidbody> Agent;
    [SerializeReference] public BlackboardVariable<ForceMode> ForceMode;

    protected override Status OnStart()
    {
        if (X.Value == null)
        {
            LogFailure("No target rigidbody assigned.");
            return Status.Failure;
        }
        Vector3 worldForce = Agent.Value.transform.TransformDirection(X.Value);
        Agent.Value.AddForce(worldForce, ForceMode.Value);
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

