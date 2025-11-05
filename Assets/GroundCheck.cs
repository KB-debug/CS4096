using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GroundCheck : MonoBehaviour
{
    public Transform groundCheckPoint; 
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; 
    public bool isGrounded;

    private BehaviorGraphAgent behaviorGraphAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);
        behaviorGraphAgent.SetVariableValue<Boolean>("Is Grounded", isGrounded);

    }
}
