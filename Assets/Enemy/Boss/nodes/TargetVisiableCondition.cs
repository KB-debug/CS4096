using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "TargetVisiable", story: "Agent can see Target", category: "Conditions", id: "5e07b7d5cf3d16afcd0d360af6c13b0c")]
public partial class TargetVisiableCondition : Condition
{
    [SerializeReference] public BlackboardVariable<bool> AgentCanSeeTarget;


    public override bool IsTrue()
    {

        if (AgentCanSeeTarget == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
