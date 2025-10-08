using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GuardAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform playerLoc;
    public float speed;
    public float turnSpeed;
    public NavMeshAgent agent;
    private Rigidbody rb;


    [Header("Detection Settings")]
    public float detectionRadius;
    public float angle;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    private bool spottedByGuard = false;

    [Header("Patrolling Setting")]
    public Transform[] patrolPoints = new Transform[0];
    public Transform patrolStart;
    private Transform target;

    //moved to PlayerStats
    //[Header("Stealth Settings")]
    //[Range(0.0f, 10.0f)]
    //public float noticeMeter;
    //[Range(0.1f, 2.0f)]
    //public float noticeSpeed;
    //private bool playerSpotted;
    private bool canSeePlayer = true;
    //[Range(0.1f, 2.0f)]
    //public float decaySpeed = 0.5f;
    private Vector3 lastKnownPlayerPosition;
    private float noticeMeter;

    [Header("Search Settings")]
    public float sweepTime;
    private float sweepAngle;


    [Header("Alert Settings")]
    public float alertRadius;
    public LayerMask guardMask;

    private GuardState currentState = GuardState.Patrolling;
    private enum GuardState
    {
        Patrolling,
        Chasing,
        Searching,
        Returning
    }





    private Renderer renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = patrolStart;
        agent.speed = speed;

        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MeterCheck();
        LookForPlayer();
        CheckStateTransitions();
        switch (currentState)
        {
            case GuardState.Patrolling:
                Patrol();
                renderer.material.color = Color.black;
                break;
            case GuardState.Chasing:
                FollowPlayer();
                AlertNearbyGuards();
                renderer.material.color = Color.red;
                
                break;
            case GuardState.Searching:
                SearchNearby();
                renderer.material.color = Color.yellow;
                break;
                //case GuardState.Returning:
                //    ReturnUpdate();
                //    break;
        }


        Debug.Log(canSeePlayer);


        //else
        //{
        //    Patrol();
        //    LookForPlayer();
        //}

    }

    private void CheckStateTransitions()
    {
        if (noticeMeter >= 10f && currentState != GuardState.Chasing && spottedByGuard)
        {
            if (DebugController.GuardLog)
                Debug.Log("Start Chasing");
            currentState = GuardState.Chasing;
            agent.isStopped = false;

        }
        else if (noticeMeter <= 2f && currentState != GuardState.Patrolling)
        {
            if (DebugController.GuardLog)
                Debug.Log("Stopping Chase");
            currentState = GuardState.Patrolling;
            agent.isStopped = false;
        }
        else if (noticeMeter >= 5f && noticeMeter < 10f && !canSeePlayer && currentState == GuardState.Chasing)
        {
            if (DebugController.GuardLog)
                Debug.Log("Start Searching");
            currentState = GuardState.Searching;
            agent.isStopped = false;


        }
        else if (noticeMeter >= 5f && noticeMeter < 10f && canSeePlayer && currentState == GuardState.Searching)
        {
            if (DebugController.GuardLog)
                Debug.Log("Resume Chase");
            currentState = GuardState.Chasing;
            agent.isStopped = false;
            noticeMeter = 10f;

        }
    }

    private void FollowPlayer()
    {

        LookAtPlayer();
        agent.isStopped = false;
        agent.SetDestination(playerLoc.position);

    }


    private void Patrol()
    {
        agent.SetDestination(target.position);


    }


    private void SearchNearby()
    {
        agent.SetDestination(lastKnownPlayerPosition);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (DebugController.GuardLog)
                Debug.Log("Sweeping");
            Sweep();
        }

        //if (canSeePlayer)
        //{
        //    lastKnownPlayerPosition = playerLoc.position;
        //}
    }


    private void LookAtPlayer()
    {
        Vector3 currentPosXZ = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 targetPosXZ = new Vector3(playerLoc.position.x, transform.position.y, playerLoc.position.z);
        Vector3 dir = (targetPosXZ - currentPosXZ).normalized;

        if (dir != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * turnSpeed);


        }
    }

    private void Sweep()
    {
        sweepAngle = Mathf.Lerp(0f, 180f, Time.deltaTime * sweepTime);
        Quaternion baseRotation = Quaternion.LookRotation(transform.forward);
        Quaternion sweepRotation = Quaternion.Euler(0, sweepAngle, 0);
        transform.rotation = baseRotation * sweepRotation;
    }



    public void UpdatePatrolPoints(Transform[] newPoints)
    {
        patrolPoints = newPoints;
        target = patrolPoints[Random.Range(0, patrolPoints.Length)];
    }


    private void LookForPlayer()
    {


        float distance = Vector3.Distance(transform.position, playerLoc.position);

        Vector3 directionToPlayer = (playerLoc.position - transform.position).normalized;
        directionToPlayer.y = 0;
        Vector3 forwardFlat = transform.forward;
        forwardFlat.y = 0;
        float angleBetween = Vector3.Angle(forwardFlat, directionToPlayer);

        if (angleBetween > angle || distance > detectionRadius)
        {
            canSeePlayer = false;
            spottedByGuard = false;
            return;
        }


        Vector3 directionToPlayerRay = (playerLoc.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, directionToPlayerRay, out RaycastHit hit, distance))
        {
            if (hit.transform != playerLoc)
            {
                canSeePlayer = false;
                spottedByGuard = false;
                if (DebugController.GuardLog)
                    Debug.Log("Hit: " + hit.collider.name);
            }
            else
            {
                canSeePlayer = true;
            }
        }

        Debug.DrawRay(transform.position, directionToPlayerRay * distance, Color.red);

        if (canSeePlayer)
        {
            if (DebugController.GuardLog)
                Debug.Log("Player Spotted");
            agent.isStopped = true;
            spottedByGuard = true;
            LookAtPlayer();
            lastKnownPlayerPosition = playerLoc.position;
        }

        //if (angleBetween < angle && distance <= detectionRadius && canSeePlayer)
        //{
        //    PlayerNoticeMeter();
        //}




    }

    private void MeterCheck()
    {
        if (canSeePlayer)
        {
            PlayerStats.AddStealth();
        }

        noticeMeter = PlayerStats.GetStealth();

        if (noticeMeter <= 0)
        {
            if (DebugController.GuardLog)
                Debug.Log("Resume Path");
            agent.isStopped = false;
        }
    }


    private void AlertNearbyGuards()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, alertRadius, guardMask);

        foreach (Collider hit in hits)
        {
            if (hit != null && hit.gameObject.CompareTag("Guard")) {
                GuardAI hitGuard = hit.gameObject.GetComponent<GuardAI>();

                hitGuard.SetLastSeenLoc(lastKnownPlayerPosition);
                hitGuard.ForceSetStateIntoChase();
                noticeMeter = PlayerStats.MaxOutStealth();
            }
        }
    }

    private void SetLastSeenLoc(Vector3 lastLoc) {
    lastKnownPlayerPosition = lastLoc;
    }

    private void ForceSetStateIntoChase()
    {
        currentState = GuardState.Chasing;
    }

    public bool PlayerBeingSeen()
    {
        return canSeePlayer;
    }


    




    void OnDrawGizmosSelected()
    {

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


    }
}
