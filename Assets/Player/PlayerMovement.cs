using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{



    [Header ("Player Movement Setting")]
    public float standSpeed = 5f;
    public float crouchSpeed = 2f;
    public float jumpStr = 1f;
    public float gravity = -9.8f;
    public float slideSpeed = 2f;

    [Header("Player Checks")]
    public CharacterController controller;
    public Transform cameraFollow;
    public Transform playerHead;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;



    private Vector3 velocity;
   

    private bool isGrounded;
    private bool isCrouching;

    private float crouchHeight;
    private float standHeight;
    private float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crouchHeight = controller.height/2;
        standHeight = controller.height;
        speed = standSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float yaw = cameraFollow.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yaw, 0);

        PlayerMoving();

        if (!isCrouching && Input.GetKeyDown(KeyCode.LeftControl))
        {
             
            Crouch();
        }
        else if (isCrouching && Input.GetKeyUp(KeyCode.LeftControl))
        {
            Stand();
        }

    }

    

    private void PlayerMoving()
    {
        Vector3 move;
        Vector3 slopeDir;
        if (IsOnSlopeLimit(out slopeDir))
        {
            move = slopeDir * slideSpeed;
        }
        else
        {
           
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            move = transform.right * x + transform.forward * z;

        


            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
                if (DebugController.PlayerLog)
                    Debug.Log("Grounded");
            }


            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(-2 * jumpStr * gravity);
                if (DebugController.PlayerLog)
                    Debug.Log("Jumping");
            }

            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

       


    }


    private bool IsOnSlopeLimit(out Vector3 slopeDirection) {

        slopeDirection = Vector3.zero;

        if (isGrounded)
        {
            
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, controller.height / 2 + 0.5f))
            {
                float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

                if (slopeAngle > controller.slopeLimit)
                {
                    
                    slopeDirection = Vector3.ProjectOnPlane(Vector3.down, hit.normal).normalized;
                    return true;
                }
            }
        }

        return false;
    }


    private void Crouch()
    {
        controller.height = crouchHeight;
        controller.center = new Vector3(0, -crouchHeight / 2, 0);
        playerHead.position = new Vector3(playerHead.position.x, playerHead.position.y - crouchHeight, playerHead.position.z);




        isCrouching = true;
        speed = crouchSpeed;

    }

    private void Stand()
    {
        controller.height = standHeight;
        controller.center = new Vector3(0, 0, 0);
        playerHead.position = new Vector3(playerHead.position.x, playerHead.position.y + crouchHeight, playerHead.position.z);

        isCrouching = false;
        speed = standSpeed;
    }
}

