using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Deploy Traps", story: "[Agent] throws out [Traps]", category: "Action", id: "e0e9ed9436bbe23601452f2a4aac04e7")]
public partial class DeployTrapsAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Traps;
    [SerializeReference] public BlackboardVariable<Transform> TrapPoint;


    [SerializeReference] public BlackboardVariable<float> UpwardsForce;
    [SerializeReference] public BlackboardVariable<float> HorizontalForce;

    protected override Status OnStart()
    {
        if (Agent?.Value == null || Traps?.Value == null || TrapPoint?.Value == null)
        {
            Debug.LogWarning("Blackboard variable not set!");
            return Status.Failure;
        }
        

        GameObject trap = UnityEngine.Object.Instantiate(Traps.Value, TrapPoint.Value.position, TrapPoint.Value.rotation);

        Rigidbody rb = trap.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 throwDirection = TrapPoint.Value.forward * HorizontalForce + Vector3.up * UpwardsForce;
            rb.linearVelocity = throwDirection;
        }


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

