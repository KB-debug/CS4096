using Unity.Behavior;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BlackboardVar : MonoBehaviour
{

    public BehaviorGraphAgent behaviorGraphAgent;
    public float backwardForce;
    public float upwardsForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 backwardsVector = -transform.forward * backwardForce + Vector3.up * upwardsForce;
        behaviorGraphAgent.SetVariableValue<Vector3>("Backwords Force", backwardsVector);
    }
}
