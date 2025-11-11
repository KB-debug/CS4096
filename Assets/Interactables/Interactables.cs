using UnityEngine;

public class Interactables : MonoBehaviour
{
    private bool playerInside = false;

    public void Interact()
    {
        if (playerInside == false)
        {
            // Enter locker
            PlayerStats.isHidden = true;
            PlayerStats.ClearStealth();
            playerInside = true;

            if (DebugController.InteractLog)
                Debug.Log("Player entered locker");
        }
        else
        {
            // Exit locker
            PlayerStats.isHidden = false;
            playerInside = false;

            if (DebugController.InteractLog)
                Debug.Log("Player exited locker");
        }
    }
}
