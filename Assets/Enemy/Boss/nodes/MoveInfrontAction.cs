using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Move Infront", story: "Set [Agent] position [Offset] from [Target]", category: "Action", id: "2bd18ac197968f04cd7c0694a82d45a0")]
public partial class MoveInfrontAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Duration;
    [Tooltip("Use Spherical linear interpolation (Slerp) instead of linear (Lerp).")]
    [SerializeReference] public BlackboardVariable<bool> UseSlerp = new BlackboardVariable<bool>(false);
    [SerializeReference] public BlackboardVariable<float> Offset;

    [CreateProperty] private float m_Progress = 0.0f;
    [CreateProperty] private Vector3 m_Origin;
    private Vector3 m_Destination;

    protected override Status OnStart()
    {
        if (Agent.Value == null || Target.Value == null)
        {
            LogFailure("No Transform or Target set.");
            return Status.Failure;
        }

        Vector3 targetForward = Target.Value.forward;
        Vector3 offsetPosition = Target.Value.position + targetForward * Offset.Value;

        if (Duration.Value <= 0.0f)
        {
            Agent.Value.position = Target.Value.position;
            return Status.Success;
        }

        m_Origin = Agent.Value.position;
        m_Destination = offsetPosition;
        m_Progress = 0;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float normalizedProgress = Mathf.Min(m_Progress / Duration.Value, 1f);

        Vector3 horizontalPosition = Vector3.Lerp(m_Origin, m_Destination, normalizedProgress);
        float jumpHeight = 2f;
        float yOffset = 4f * jumpHeight * normalizedProgress * (1f - normalizedProgress);

        Agent.Value.position = new Vector3(horizontalPosition.x, horizontalPosition.y + yOffset, horizontalPosition.z);

        m_Progress += Time.deltaTime;

        return normalizedProgress == 1 ? Status.Success : Status.Running;
    }

    protected override void OnDeserialize()
    {
        // Only target to reduce serialization size.
        m_Destination = Target.Value.position;
    }
}

