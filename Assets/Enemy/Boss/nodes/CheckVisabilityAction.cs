using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckVisability", story: "Finds if [Agent] can see [Target]", category: "Action", id: "c482473d8d1300b2b1b983db94b43f5b")]
public partial class CheckVisabilityAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<bool> AgentCanSeeTarget;

    [SerializeReference] public BlackboardVariable<float> DetectionRange = new BlackboardVariable<float>(1.0f);
    [SerializeReference] public BlackboardVariable<String> TargetLayerName = new BlackboardVariable<string>();
    [SerializeReference] public BlackboardVariable<string> IgnoreLayerName = new BlackboardVariable<string>();

    private int playerLayerMask;
    private int raycastLayerMask;
    protected override Status OnStart()
    {
        playerLayerMask = LayerMask.GetMask(TargetLayerName.Value);
        int ignoreLayer = LayerMask.GetMask(IgnoreLayerName.Value);
        raycastLayerMask = ~ignoreLayer;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {

        

        if (Agent?.Value == null || Target?.Value == null)
        {
            AgentCanSeeTarget.Value = false;
            return Status.Failure;
        }

        Collider[] playersInRange = Physics.OverlapSphere(Agent.Value.transform.position, DetectionRange, playerLayerMask);

        if (playersInRange.Length <= 0) {
            AgentCanSeeTarget.Value = false;
            return Status.Success;
        }


        foreach (Collider collider in playersInRange) { 
        GameObject target = collider.gameObject;

            if (target != null && target == Target)
            {
                Vector3 directionToPlayer = (Target.Value.transform.position - Agent.Value.transform.position).normalized;
                if (Physics.Raycast(Agent.Value.transform.position, directionToPlayer, out RaycastHit hit, DetectionRange.Value,raycastLayerMask)) {

                    if (hit.collider.gameObject == Target.Value)
                    {
                        AgentCanSeeTarget.Value = true;
                    }
                }


            }
        }
        return Status.Success;


       
    }

    protected override void OnEnd()
    {
    }
}

