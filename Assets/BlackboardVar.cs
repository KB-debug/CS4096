using Unity.Behavior;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BlackboardVar : MonoBehaviour
{

    public BehaviorGraphAgent behaviorGraphAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 backwardsVector = -transform.forward * 2f + Vector3.up * 8f;
        behaviorGraphAgent.SetVariableValue<Vector3>("Backwords Force", backwardsVector);
        Debug.Log("Trying to change var");
    }
}
