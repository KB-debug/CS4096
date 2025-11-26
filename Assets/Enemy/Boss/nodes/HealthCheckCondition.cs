using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Health Check", story: "[BossHealth] health [Operator] [Threshold]", category: "Conditions", id: "e88d96b8607936c6534b0cfdc81f7490")]
public partial class HealthCheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> BossHealth;
    [Comparison(comparisonType: ComparisonType.All)]
    [SerializeReference] public BlackboardVariable<ConditionOperator> Operator;
    [SerializeReference] public BlackboardVariable<float> Threshold;

    public override bool IsTrue()
    {
        if (BossHealth?.Value != null && Threshold?.Value != null)
        {

            return ConditionUtils.Evaluate(BossHealth,Operator,Threshold);


        }


        return false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
