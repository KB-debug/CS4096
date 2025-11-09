using UnityEngine;

public class Expand : MonoBehaviour
{
    public float expandSpeed = 1f;
    public float damage = 99999f;

    public float maxSize = 200f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;

        if (transform.localScale.x > maxSize)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);

        if (other.gameObject.CompareTag("Player"))
        {




            GameObject player = other.gameObject;

            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hit ,distanceToPlayer ))
            {
                if (hit.transform == player.transform)
                {
                    PlayerStats.PlayerTakeDamage(damage);
                }
            }
            
        }
    }
}
