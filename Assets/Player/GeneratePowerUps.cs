
using UnityEngine;

using System.Collections;
public class GeneratePowerUps : MonoBehaviour
{
    public GameObject[] powerUps;

    public float timeToRespawn;

    private GameObject currentPowerup;
    private Vector3 spawnPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPos = transform.position;
        spawnPos.y = 3f;
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnPowerup()
    {

        GameObject powerupPrefab = powerUps[Random.Range(0, powerUps.Length)];

        currentPowerup = Instantiate(powerupPrefab, spawnPos, transform.rotation);

        LogSpawn powerup = currentPowerup.GetComponent<LogSpawn>();
        if (powerup != null)
        {
            powerup.spawner = this;
        }
    }

    public void StartRespawnTimer()
    {
        StartCoroutine(RespawnRoutine());
    }
    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(timeToRespawn);
        SpawnPowerup();
    }

    
}
