using UnityEngine;

public class DespawnTImer : MonoBehaviour
{
    public float timeToDespawn = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToDespawn -= Time.deltaTime;
        if (timeToDespawn < 0) {
            Destroy(gameObject);
        }
    }
}
