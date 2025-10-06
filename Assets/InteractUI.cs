using UnityEngine;

public class InteractUI : MonoBehaviour
{
    public GameObject interactButton;
    public PlayerInteraction playerInteraction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInteraction.GetInteractables() != null)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {

        interactButton.SetActive(true);
    }

    private void Hide()
    {
        interactButton?.SetActive(false);   
    }
}
