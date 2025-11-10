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
    public string[] enemyTags = { "Drone", "Guard" };

    [Header("NavMesh Agent")]
    public NavMeshAgent agent;

    private void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player")?.transform;

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
                if (!enemy) continue;

                Outline outline = enemy.GetComponent<Outline>();
                if (!outline) continue;

                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                // ✅ Highlight if in range, disable if out of range
                if (distance <= enemyDetectionRadius)
                {
                    outline.enabled = true;
                    Debug.DrawLine(transform.position, enemy.transform.position, Color.red);
                }
                else
                {
                    outline.enabled = false;
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Vector3 forward = transform.forward * detectionRadius;
        Quaternion leftRotation = Quaternion.Euler(0, -angle, 0);
        Quaternion rightRotation = Quaternion.Euler(0, angle, 0);
        Vector3 leftDir = leftRotation * forward;
        Vector3 rightDir = rightRotation * forward;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + leftDir);
        Gizmos.DrawLine(transform.position, transform.position + rightDir);
        */
    }
}
