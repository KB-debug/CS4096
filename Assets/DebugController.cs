using UnityEngine;

public class DebugController : MonoBehaviour
{
   
    public bool playerLog = true;
    public bool droneLog = true;

    public static bool PlayerLog;
    public static bool DroneLog;

    private void Awake()
    {
        PlayerLog = playerLog;
        DroneLog = droneLog;
    }

    private void OnValidate()
    {
        PlayerLog = playerLog;
        DroneLog = droneLog;
    }
}
