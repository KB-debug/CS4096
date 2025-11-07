using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "JumpBackwards", story: "[Agent] Jumps Backwards", category: "Action", id: "54024c19db77f1be813ed67d121138fd")]
public partial class JumpBackwardsAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Agent;
    [SerializeReference] public BlackboardVariable<int> JumpCount;
    [SerializeReference] public BlackboardVariable<Boolean> CanEscape;
    [SerializeReference] public BlackboardVariable<int> MaxJump;
    [SerializeReference] public BlackboardVariable<float> jumpDistance;   // how far the boss jumps
    [SerializeReference] public BlackboardVariable<float> jumpHeight;     // height of the arc
    [SerializeReference] public BlackboardVariable<float> jumpDuration; // time of the leap

    private bool isJumping = false;

    protected override Status OnStart()
    {
        if (Agent?.Value == null)
            return Status.Failure;

        if (JumpCount.Value >= MaxJump.Value)
        {
            CanEscape.Value = false;
            return Status.Failure;
        }



        Transform agent = Agent.Value;
        Vector3 backwardDir = -agent.forward;
        Vector3 startPos = agent.position;

        // List of candidate jump directions
        Vector3[] directions = new Vector3[]
        {
            backwardDir, // straight back
            (backwardDir + -agent.right).normalized, // back-left
            (backwardDir + agent.right).normalized   // back-right
        };

        Vector3 targetPos = startPos; // fallback if all blocked
        bool foundSafeSpot = false;

        foreach (var dir in directions)
        {
            // Check forward for obstacles
            if (!Physics.Raycast(startPos, dir, jumpDistance.Value))
            {
                targetPos = startPos + dir * jumpDistance.Value;
                foundSafeSpot = true;
                break;
            }
        }

        if (!foundSafeSpot)
        {
            // fallback: just stay in place, cannot jump
            Debug.Log("JumpBackwardsAction: No safe jump position found!");
            return Status.Failure;
        }

        // Start the leap coroutine
        Agent.Value.GetComponent<MonoBehaviour>().StartCoroutine(LeapAlongCurve(Agent.Value, targetPos, jumpHeight.Value, jumpDuration.Value));
        isJumping = true;
       
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return isJumping ? Status.Running : Status.Success;
    }

    protected override void OnEnd()
    {
        isJumping = false;
    }

    private IEnumerator LeapAlongCurve(Transform agent, Vector3 target, float height, float duration)
    {
        Vector3 startPos = agent.position;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;

            // Parabolic interpolation
            Vector3 pos = Vector3.Lerp(startPos, target, normalized);
            pos.y += height * 4f * normalized * (1 - normalized); // simple parabola
            agent.position = pos;

            yield return null;
        }

        // Ensure exact landing position
        agent.position = target;
        isJumping = false;
        
    }
}
