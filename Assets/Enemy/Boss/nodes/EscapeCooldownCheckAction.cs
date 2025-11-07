using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EscapeCooldownCheck", story: "Update [CanEscape] Cooldown", category: "Action", id: "b990dd1afd06e410ea593735116b643b")]
public partial class EscapeCooldownCheckAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<bool> CanEscape;
    [SerializeReference] public BlackboardVariable<float> TimeTillReset;
    [SerializeReference] public BlackboardVariable<int> JumpCount;

    private float t;
    private bool hasReset = false;
    protected override Status OnStart()

    {

        if (Agent?.Value == null || CanEscape == null || TimeTillReset == null || JumpCount == null)
        {
            Debug.Log("Missing Componets");
            return Status.Failure;
        }

        t = 0f;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        
        t += Time.deltaTime;
        Debug.Log(t);

        while (t < TimeTillReset) { 
            return Status.Running;
        }
        JumpCount.Value = 0;

        return Status.Success;
    }

    protected override void OnEnd()
    {
        
    }

    //private IEnumerator Countdown()
    //{
    //    yield return new WaitForSeconds(TimeTillReset.Value);
    //    JumpCount.Value = 0;
    //    CanEscape.Value = true;
    //    hasReset = true;
    //}
}

