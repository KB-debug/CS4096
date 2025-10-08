using UnityEngine;
using UnityEngine.AI;

public class CompanionAI : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform player;          
    public float followDistance = 2f;
    public float stoppingDistance = 1f;
    public float turnSpeed = 5f;

    [Header("Detection Settings")]
    public float enemyDetectionRadius = 10f;
    public string[] enemyTags = { "Drone", "Guard" };

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

        // Going to add better obstical clearance
        if (distance > followDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position - direction.normalized * 1f); // Keeps distance
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

    private void DetectEnemies() // Planning on making it line of sight based
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, enemyDetectionRadius);

        foreach (Collider enemy in enemies)
        {
            foreach (string tag in enemyTags)
            {
                if (enemy.CompareTag(tag))
                {
                    Debug.DrawLine(transform.position, enemy.transform.position, Color.red);
                    // Planning on adding a FSM or Behavior tree on what to do when an enemy is spotted
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
    }
}
