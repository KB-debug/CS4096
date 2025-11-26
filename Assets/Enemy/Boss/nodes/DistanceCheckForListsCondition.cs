using System;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Distance Check For Lists", story: "distance between [TargetA] and [TargetBList] [Operator] than [Distance]", category: "Conditions", id: "7381a565dca58b151657b3b90af37db1")]
public partial class DistanceCheckForListsCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> TargetA;
    [SerializeReference] public BlackboardVariable<GameObject[]> TargetBList;
    [Comparison(comparisonType: ComparisonType.All)]
    [SerializeReference] public BlackboardVariable<ConditionOperator> Operator;
    [SerializeReference] public BlackboardVariable<float> Distance;

    [SerializeReference] public BlackboardVariable<GameObject> ClosestTarget;
    public override bool IsTrue()
    {
        if (TargetA?.Value == null || TargetBList?.Value == null || TargetBList.Value.Length == 0)
            return false;

        foreach (GameObject target in TargetBList.Value) { 
        
        }

        float closestDistance = float.MaxValue;
        GameObject closest = null;

        foreach (GameObject target in TargetBList.Value)
        {
            if (target == null) continue;

            float dist = Vector3.Distance(TargetA.Value.position, target.transform.position);

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = target;
            }

        }
        ClosestTarget.Value = closest;


        return ConditionUtils.Evaluate(closestDistance, Operator, Distance.Value);
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
