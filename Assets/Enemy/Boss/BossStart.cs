using UnityEngine;

public class BossStart : MonoBehaviour
{
    [SerializeField] private float extraGravity = 300f;

    private Rigidbody rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
        rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
        
    }
}
