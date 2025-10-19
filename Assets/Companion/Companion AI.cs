using UnityEngine;
using UnityEngine.AI;

public class CompanionAI : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform player;
    public float followDistance = 2f;
    public float stoppingDistance = 1f;
    public float turnSpeed = 5f;

    [Header("Enemy Detection Settings")]
    public float enemyDetectionRadius = 10f;
    // public float viewAngle = 100f; 
    public string[] enemyTags = { "Drone", "Guard" };
    public LayerMask obstacleMask;

    [Header("NavMesh Agent")]
    public NavMeshAgent agent;

    private void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = stoppingDistance;
    }

    private void Update()
    {
        FollowPlayer();
        DetectEnemies();
    }

    private void FollowPlayer()
    {
        if (!player) return;

        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (distance > followDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position - direction.normalized * 1f);
        }
        else
        {
            agent.isStopped = true;
        }

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
    }

    private void DetectEnemies()
    {
        foreach (string tag in enemyTags)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject enemy in enemies)
            {
                Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy > enemyDetectionRadius)
                    continue;

                
                // float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);
                // if (angleToEnemy > viewAngle / 2f)
                // continue;

                if (!Physics.Raycast(transform.position + Vector3.up * 1f, directionToEnemy, distanceToEnemy, obstacleMask))
                {
                    // Enemy Detection Reaction:
                    Debug.DrawLine(transform.position, enemy.transform.position, Color.red); // Clean up later

                    Outline outline = enemy.GetComponent<Outline>();
                    if (outline != null)
                    {
                        outline.enabled = true; // Might add a fade quick fade out so it doesnt look choppy

                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, followDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRadius);

        
        /*
        Gizmos.color = Color.yellow;
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * enemyDetectionRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * enemyDetectionRadius);
        */
    }
}