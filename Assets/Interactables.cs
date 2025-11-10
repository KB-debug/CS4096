using UnityEngine;

public class Interactables : MonoBehaviour
{

    [SerializeField] private typeOfInteractable whatInteractable;


    enum typeOfInteractable
    {
        Locker,
        AttackUp,
        Hp
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Interact()
    {
        if (DebugController.InteractLog)
            Debug.Log("Interaction Occured");

        switch (whatInteractable)
        {
            case typeOfInteractable.Locker:
                PlayerStats.ClearStealth();
                return;

            case typeOfInteractable.AttackUp:
                
                Debug.Log("Attack Type");
                BladeCollision.InceaseDamage(1);
                Destroy(gameObject);
                return;

            case typeOfInteractable.Hp:

                Debug.Log("HP Type");
                PlayerStats.PlayerHeal(5);
                Destroy(gameObject);
                return;

        }

        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss")){

            Destroy(gameObject);
        }
    }
}
