using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DroneAI : MonoBehaviour
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
    private bool spottedByDrone = false;

    [Header("Patrolling Setting")]
    public Transform[] patrolPoints = new Transform[0];
    public Transform patrolStart;
    private Transform target;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootingRange = 10f;
    public float fireRate = 1f;
    private float fireCooldown = 0f;

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
    private static float noticeMeter;

    [Header("Search Settings")]
    public float sweepTime;
    private float sweepAngle;

    private static GuardState currentState = GuardState.Patrolling;
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
        renderer = GetComponent<Renderer>();
        playerLoc = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        target = patrolStart;
        agent.speed = speed;

       

      
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
                renderer.material.color = Color.red;
                break;
            case GuardState.Searching:
                SearchNearby();
                renderer.material.color = Color.yellow;
                break;
                //case DroneState.Returning:
                //    ReturnUpdate();
                //    break;
        }


        
        //else
        //{
        //    Patrol();
        //    LookForPlayer();
        //}

    }

    private void CheckStateTransitions()
    {
        if (noticeMeter >= 10f && currentState != GuardState.Chasing && spottedByDrone)
        {
            if (DebugController.DroneLog)
                Debug.Log("Start Chasing");
            currentState = GuardState.Chasing;
            agent.isStopped = false;
            
        }
        else if (noticeMeter <= 2f && currentState != GuardState.Patrolling)
        {
            if (DebugController.DroneLog)
                Debug.Log("Stopping Chase");
            currentState = GuardState.Patrolling;
            agent.isStopped = false;
        }
        else if (noticeMeter >= 5f && noticeMeter < 10f && !canSeePlayer && currentState == GuardState.Chasing)
        {
            if (DebugController.DroneLog)
                Debug.Log("Start Searching");
            currentState = GuardState.Searching;
            agent.isStopped = false;
            

        }
        else if (noticeMeter >= 5f && noticeMeter < 10f && canSeePlayer && currentState == GuardState.Searching)
        {
            if (DebugController.DroneLog)
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

        float distanceToPlayer = Vector3.Distance(transform.position, playerLoc.position);
        if (distanceToPlayer <= shootingRange)
        {
            ShootAtPlayer();
        }

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
            if (DebugController.DroneLog)
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
        sweepAngle = Mathf.Lerp(0f,360f,Time.deltaTime * sweepTime);
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
            spottedByDrone = false;
            return; 
        }


        Vector3 directionToPlayerRay = (playerLoc.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, directionToPlayerRay, out RaycastHit hit, distance))
        {
            if (hit.transform != playerLoc)
            {
                canSeePlayer = false;
                spottedByDrone = false;
                if (DebugController.DroneLog)
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
            if (DebugController.DroneLog)
                Debug.Log("Player Spotted");
            agent.isStopped = true;
            spottedByDrone = true;
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
            if (DebugController.DroneLog)
                Debug.Log("Resume Path");
            agent.isStopped = false;
        }
    }


    public bool PlayerBeingSeen() 
    {
        return canSeePlayer;
    }

    private void ShootAtPlayer()
    {
        if (fireCooldown <= 0f)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            bullet.transform.forward = (playerLoc.position - firePoint.position).normalized;

            fireCooldown = 1f / fireRate;
        }
        else
        {
            fireCooldown -= Time.deltaTime;
        }
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
