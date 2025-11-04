using UnityEngine;
using Unity.Behavior;

public class DebuggingWireFrame : MonoBehaviour
{
    private BehaviorGraphAgent behaviorAgent;
    public string detectionRangeVariableName = "DetectionRange"; 
    public Color gizmoColor = Color.red;

    private void Awake()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
    }


    private void OnDrawGizmosSelected()
    {
        
        float detectionRange = 10f;

        if (behaviorAgent != null && behaviorAgent.BlackboardReference != null)
        {
           
            if (behaviorAgent.BlackboardReference.GetVariableValue(detectionRangeVariableName, out float range))
            {
                detectionRange = range;
            }
        }

        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}