using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Look At Predicted Spot", story: "[Agent] looks at [Player] Movement", category: "Action", id: "81a052a8b80b8465106b0d8f5aeec46f")]
public partial class LookAtPredictedSpotAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Player;
    [Tooltip("True: the node process the LookAt every update with status Running." +
        "\nFalse: the node process the LookAt only once.")]
    [SerializeReference] public BlackboardVariable<bool> Continuous = new BlackboardVariable<bool>(false);
    [SerializeReference] public BlackboardVariable<bool> LimitToYAxis = new BlackboardVariable<bool>(false);
    [SerializeReference] public BlackboardVariable<float> BulletSpeed;

    protected override Status OnStart()
    {
        if (Agent.Value == null || Player.Value == null)
        {
            LogFailure($"Missing Transform or Target.");
            return Status.Failure;
        }

        ProcessLookAt();
        return Continuous.Value ? Status.Running : Status.Success;
    }

    protected override Status OnUpdate()
    {
        if (Continuous.Value)
        {
            ProcessLookAt();
            return Status.Running;
        }
        return Status.Success;
    }

    void ProcessLookAt()
    {

        Transform target = Player.Value;
        Transform self = Agent.Value;

        if (target == null || self == null)
        {
            return;
        }

        Vector3 targetPosition = Player.Value.position;

        PlayerMovement playerMove = target.GetComponent<PlayerMovement>();
        if (playerMove != null)
        {

            Vector3 playerVel = playerMove.Velocity;

            float bulletSpeed = BulletSpeed.Value;
            float distance = Vector3.Distance(self.position, target.position);
            float timeToTarget = distance / bulletSpeed;

            targetPosition += playerVel/3 * timeToTarget;
        }

        Vector3 direction = (targetPosition - self.position).normalized;
        if (LimitToYAxis.Value)
        {
            targetPosition.y = self.position.y;
            

        }

  

        // Create rotation that looks at target without rolling
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            self.rotation = targetRotation;
        }
    }
}


