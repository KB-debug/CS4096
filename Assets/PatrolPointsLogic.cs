using UnityEngine;

public class PatrolPointsLogic : MonoBehaviour
{
    [Header ("Nearby Patrol Points")]
    public Transform[] nearbyPoints = new Transform[0];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(transform.position.x, 6f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision != null && collision.gameObject.CompareTag("Drone"))
    //    {
    //        DroneAI obj = collision.gameObject.GetComponent<DroneAI>();
    //        if (DebugController.DroneLog)
    //            Debug.Log("Drone Entered");

    //        if (obj != null)
    //        {
    //            obj.UpdatePatrolPoints(nearbyPoints);
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other != null && other.gameObject.CompareTag("Drone"))
        {
            DroneAI obj = other.gameObject.GetComponent<DroneAI>();
            if (DebugController.DroneLog)
                Debug.Log("Drone Entered");

            if (obj != null)
            {
                obj.UpdatePatrolPoints(nearbyPoints);
                if (DebugController.DroneLog)
                    Debug.Log("DronePathUpdated");
            }
        }
    }
}
