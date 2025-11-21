using UnityEngine;

public class DebugController : MonoBehaviour
{
   
    public bool playerLog = true;
    public bool droneLog = true;
    public bool guardLog = true;
    public bool interactLog = true;

    public static bool PlayerLog;
    public static bool DroneLog;
    public static bool GuardLog;
    public static bool InteractLog;

    private void Awake()
    {
        PlayerLog = playerLog;
        DroneLog = droneLog;
        GuardLog = guardLog;
        InteractLog = interactLog;
    }

    private void OnValidate()
    {
        PlayerLog = playerLog;
        DroneLog = droneLog;
        GuardLog= guardLog;
        InteractLog = interactLog;
    }
}
