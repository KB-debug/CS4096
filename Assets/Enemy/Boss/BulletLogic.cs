using Unity.Behavior;
using UnityEngine;

public class BulletLogicBoss : MonoBehaviour
{
    public float speed;
    public float damage;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats.PlayerTakeDamage(damage);
        }
    }
}
