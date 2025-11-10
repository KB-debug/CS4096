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


    private BossStart bs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
        bs = GetComponent<BossStart>();
    }

    // Update is called once per frame
    void Update()
    {
       
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);
        behaviorGraphAgent.SetVariableValue<Boolean>("IsGrounded", isGrounded);


        if (isGrounded) {
            bs.enabled = false;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (groundCheckPoint == null) return;

    //    Gizmos.color = isGrounded ? Color.green : Color.red;


    //    Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);

    //    Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.3f);
    //    Gizmos.DrawSphere(groundCheckPoint.position, groundCheckRadius * 0.8f);
    //}

}
