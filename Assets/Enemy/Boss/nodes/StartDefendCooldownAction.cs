using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Start Defend Cooldown", story: "Start Cooldown for [CanDefend]", category: "Action", id: "dc46b2ade04c056d7138f1a43978a020")]
public partial class StartDefendCooldownAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> CanDefend;
    [SerializeReference] public BlackboardVariable<float> CooldownDuration;

    private float t;
    protected override Status OnStart()
    {

        if (CanDefend == null || CooldownDuration == null)
        {
            Debug.Log("No Boolean to change or No Cooldown to countdown");
            return Status.Failure;
        }

        t = 0f;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        t += Time.deltaTime;

        while (t < CooldownDuration)
        {
            
            return Status.Running;
        }

        CanDefend.Value = true;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

