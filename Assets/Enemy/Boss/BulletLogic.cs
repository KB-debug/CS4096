using UnityEngine;

public class BulletLogic : MonoBehaviour
{

    public float speed = 5f;

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
