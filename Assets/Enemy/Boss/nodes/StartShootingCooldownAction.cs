using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Start Shooting Cooldown", story: "Start Cooldown for [ShootingCooldown]", category: "Action", id: "dc0d6bf8e3f4e60d0d6d129b5cd4fe68")]
public partial class StartShootingCooldownAction : Action
{
    [SerializeReference] public BlackboardVariable<float> ShootingCooldown;

    private float t;
    protected override Status OnStart()
    {
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {

        ShootingCooldown.Value -= Time.deltaTime;

        while (ShootingCooldown > 0)
        {
            return Status.Running;
        }

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

