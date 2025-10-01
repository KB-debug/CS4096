using UnityEngine;

public class CamFollow : MonoBehaviour
{

    public GameObject playerPivot;

    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPivot.transform.position + offset;
        transform.rotation = playerPivot.transform.rotation; 

    }
}
