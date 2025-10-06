using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public float interactRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Interactables interactables))
                {
                    interactables.Interact();
                }
            }
        }
    }

    public Interactables GetInteractables()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Interactables interactables))
            {
                return interactables;   
            }
        }

        return null;
    }
}
