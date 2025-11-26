using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 20f;   // How fast the bullet moves
    public float damage = 10f;  // Damage dealt to the player
    public float lifetime = 5f; // Time before the bullet destroys itself

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speed; // Move the bullet forward
        }

        Destroy(gameObject, lifetime); // Auto destroy after lifetime
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet collided with: " + other.gameObject.name);

        // Damage player on collision
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player for " + damage);
            PlayerStats.PlayerTakeDamage(damage);
        }

        // Destroy bullet on any collision
        Destroy(gameObject);
    }
}
