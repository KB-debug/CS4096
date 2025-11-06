using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Special Jump Attack", story: "[Agent] jumps to [Location] and causes [Attack]", category: "Action", id: "6d149c65722d5d2a843dd3af79b43220")]
public partial class SpecialJumpAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Location;
    [SerializeReference] public BlackboardVariable<GameObject> Attack;

    [SerializeReference] public BlackboardVariable<float> jumpHeight;
    [SerializeReference] public BlackboardVariable<float> jumpDuration;

    private float elapsed;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool initialized;

    protected override Status OnStart()
    {

        if (Agent?.Value == null || Location?.Value == null)
            return Status.Failure;

        startPos = Agent.Value.position;
        endPos = Location.Value.position;
        elapsed = 0f;
        initialized = true;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!initialized)
            return Status.Failure;

        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / jumpDuration);

        float heightOffset = 4 * jumpHeight * t * (1 - t);
        Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);
        currentPos.y += heightOffset;

        Agent.Value.position = currentPos;

        if (t >= 1f)
        {
            if (Attack?.Value != null)
            {
                UnityEngine.Object.Instantiate(Attack.Value,Agent.Value.position,Quaternion.identity);
            }

        }
            return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

