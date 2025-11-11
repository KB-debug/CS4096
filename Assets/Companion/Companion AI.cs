using UnityEngine;
using UnityEngine.AI;

public class CompanionAI : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform player;
    public float followDistance = 2f;
    public float stoppingDistance = 1f;
    public float turnSpeed = 5f;

    [Header("Idle Settings")]
    public float idleWanderRadius = 3f;
    public float idleBreakDistance = 2.5f;
    public float playerStillThreshold = 0.05f;
    public float timeToIdle = 5f;
    public float idleMinDistanceFromPlayer = 3f;

    private float stillTimer = 0f;
    private Vector3 lastPlayerPosition;

    [HideInInspector] public Vector3 idleOrigin;

    [Header("Enemy Detection Settings")]
    public float enemyDetectionRadius = 10f;
    public string[] enemyTags = { "Drone", "Guard" };

    [Header("NavMesh Agent")]
    public NavMeshAgent agent;

    
    [HideInInspector] public FollowState followState;
    [HideInInspector] public IdleState idleState;
    private CompanionState currentState;

    public MeshRenderer meshRenderer;

   


    private void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player")?.transform;

        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = stoppingDistance;

       
        followState = new FollowState(this);
        idleState = new IdleState(this);

       
        SwitchState(followState);

        lastPlayerPosition = player.position;
    }

    private void Update()
    {
        UpdatePlayerStillness();
        currentState.Update();

        DetectEnemies();
    }

    public void SwitchState(CompanionState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    private void UpdatePlayerStillness()
    {
        float movement = Vector3.Distance(player.position, lastPlayerPosition);

        if (movement < playerStillThreshold)
            stillTimer += Time.deltaTime;
        else
            stillTimer = 0f;

        lastPlayerPosition = player.position;
    }

    public bool PlayerStoppedLongEnough() => stillTimer >= timeToIdle;

    public void FollowPlayerLogic()
    {
        if (!player) return;

        Vector3 dir = player.position - transform.position;
        float distance = dir.magnitude;

        if (distance > followDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position - dir.normalized * 1f);
        }
        else
        {
            agent.isStopped = true;
        }

        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
    }

    private Vector3 currentTarget;

    public void Wander(Vector3 target)
    {
        if (target != currentTarget)
        {
            agent.isStopped = false;
            agent.SetDestination(target);
            currentTarget = target;
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

    public void SetVisibility(bool visible)
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = visible;
           // meshRenderer.shadowCastingMode = visible
                //? UnityEngine.Rendering.ShadowCastingMode.On
                //: UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }



    private void OnDrawGizmosSelected()
    {
        Vector3 origin = idleOrigin;
        if (origin == Vector3.zero && player != null)
            origin = player.position;

        //idle break distance 
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, idleBreakDistance);

        // wander radius 
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(origin, idleWanderRadius);

        // current wander target
        if (agent != null && agent.hasPath)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, agent.destination);
            Gizmos.DrawSphere(agent.destination, 0.2f);
        }
    }



}


