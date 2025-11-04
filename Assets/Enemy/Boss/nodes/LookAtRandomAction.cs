using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Look At Random", story: "[Agent] looks at [Points] Randomly", category: "Action", id: "ad28c05608ab308cf448ae083def17ab")]
public partial class LookAtRandomAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Agent;
    [SerializeReference] public BlackboardVariable<List<GameObject>> Points;
    [SerializeReference] public BlackboardVariable<bool> LimitToYAxis = new BlackboardVariable<bool>(false);

    protected override Status OnStart()
    {
        if (Agent.Value == null || Points.Value == null)
        {
            LogFailure($"Missing Transform or Target.");
            return Status.Failure;
        }

        ProcessLookAt();
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    void ProcessLookAt()
    {
        int ranIndex = UnityEngine.Random.Range(0,Points.Value.Count);

        GameObject Target = Points.Value[ranIndex];

        Vector3 targetPosition = Target.transform.position;

        if (LimitToYAxis.Value)
        {
            targetPosition.y = Agent.Value.transform.position.y;
        }
        Agent.Value.LookAt(targetPosition, Agent.Value.up);
    }
}

