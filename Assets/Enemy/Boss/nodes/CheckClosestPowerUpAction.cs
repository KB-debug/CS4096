using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Check Closest Power Up", story: "Check Which [PowerUp] is Closest", category: "Action", id: "f5e2dbf521535cde7420fc70b6723d0b")]
public partial class CheckClosestPowerUpAction : Action
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> PowerUp;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<GameObject> ClosestTarget;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float closestDistance = float.MaxValue;
        GameObject closest = null;

        foreach (GameObject target in PowerUp.Value)
        {
            if (target == null) continue;

            float dist = Vector3.Distance(Target.Value.position, target.transform.position);

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = target;
            }
        }
        ClosestTarget.Value = closest;

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

