using UnityEngine;

public class BladeCollision : MonoBehaviour
{

    public float damage;
    private static float damageS;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damageS = damage;
    }

    // Update is called once per frame
    void Update()
    {
        damage = damageS;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            BossTakeDamage.TakeDamage(damage);
        }
    }

    public static void InceaseDamage(float amount) 
    {
        damageS += amount;
    }
}
