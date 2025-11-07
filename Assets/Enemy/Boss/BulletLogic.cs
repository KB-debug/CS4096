using UnityEngine;

public class BulletLogic : MonoBehaviour
{

    public float speed = 5f;

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 force = transform.forward * speed * Time.deltaTime;

        rb.AddForce(force);
    }
}
