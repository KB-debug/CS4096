using UnityEngine;

public class Interactables : MonoBehaviour
{
    public Camera lockerCamera;
    private bool playerInside = false;
    public Camera playerCamera;

    public CompanionAI companion;

    public void Interact()
    {
        if (!playerInside)
            EnterLocker();
        else
            ExitLocker();
    }

    private void EnterLocker()
    {
        PlayerStats.isHidden = true;
        PlayerStats.ClearStealth();
        playerInside = true;

        // Disable player model
        PlayerRenderer(false);

        companion.SetVisibility(false);

        // Switch camera
        SwitchToLockerCamera(true);

        Debug.Log("Player ENTERED locker");
    }

    private void ExitLocker()
    {
        PlayerStats.isHidden = false;
        playerInside = false;

        // Re-enable player model
        PlayerRenderer(true);

        companion.SetVisibility(true);


        // Restore main player camera
        SwitchToLockerCamera(false);

        Debug.Log("Player EXITED locker");
    }

    private void PlayerRenderer(bool enable)
    {
        
        GameObject playerObj = GameObject.FindWithTag("Player");
        Renderer[] rends = playerObj.GetComponentsInChildren<Renderer>();

        foreach (Renderer r in rends)
            r.enabled = enable;
    }

    private void SwitchToLockerCamera(bool entering)
    {
        if (entering)
        {
            playerCamera.enabled = false;
            lockerCamera.enabled = true;
        }
        else
        {
            lockerCamera.enabled = false;
            playerCamera.enabled = true;
        }
    }


}
