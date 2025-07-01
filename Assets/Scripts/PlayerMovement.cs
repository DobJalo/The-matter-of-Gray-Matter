using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;

    private bool reversecontrols = false;

    public Transform cameraTransform;
    private Rigidbody rb;
    private float xRotation = 0f;

    public GameObject plane1; //first platform
    public GameObject plane2; //first checkpoint
    public GameObject plane3; //second checkpoint
    public GameObject plane4;
    public GameObject plane5;
    public GameObject plane6;
    public GameObject plane7;
    public GameObject ending;

    public GameObject MenuObject;
    private bool moveAround = false;

    public Camera playerCamera;

    //jumping
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;
    private bool isGrounded;

    void Start()
    {
        playerCamera.farClipPlane = 1000;
        speed = 5;

        rb = GetComponent<Rigidbody>(); // Get Rigidbody component from Player

        //start position (checkpoints)
        //place player on a particular checkpoint based on previous saves
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            if (PlayerPrefs.GetInt("Checkpoint") == 1)
            {
                this.gameObject.transform.position = new Vector3(plane2.transform.position.x, plane2.transform.position.y + 2, plane2.transform.position.z); 
            }
            if (PlayerPrefs.GetInt("Checkpoint") == 2)
            {
                this.gameObject.transform.position = new Vector3(plane3.transform.position.x, plane3.transform.position.y + 2, plane3.transform.position.z);
            }
            if (PlayerPrefs.GetInt("Checkpoint") == 3)
            {
                this.gameObject.transform.position = new Vector3(plane4.transform.position.x, plane4.transform.position.y + 2, plane4.transform.position.z);
            }
            if (PlayerPrefs.GetInt("Checkpoint") == 4)
            {
                this.gameObject.transform.position = new Vector3(plane5.transform.position.x, plane5.transform.position.y + 2, plane5.transform.position.z);
            }
            if (PlayerPrefs.GetInt("Checkpoint") == 5)
            {
                this.gameObject.transform.position = new Vector3(plane6.transform.position.x, plane6.transform.position.y + 2, plane6.transform.position.z);
            }
            if (PlayerPrefs.GetInt("Checkpoint") == 6)
            {
                this.gameObject.transform.position = new Vector3(plane7.transform.position.x, plane7.transform.position.y + 2, plane7.transform.position.z);
            }
            if (PlayerPrefs.GetInt("Checkpoint") == 7)
            {
                this.gameObject.transform.position = new Vector3(ending.transform.position.x, ending.transform.position.y + 2, ending.transform.position.z);
            }
        }
        else
        {
            this.gameObject.transform.position = new Vector3(plane1.transform.position.x, plane1.transform.position.y + 2, plane1.transform.position.z);
        }


    }

    void FixedUpdate() 
    {
        // Player movement
        //controls are reversed on purpose 
        if (Input.GetKeyDown(KeyCode.M) && reversecontrols == false)
        {
            reversecontrols = true;
        }
        else if (Input.GetKeyDown(KeyCode.M) && reversecontrols == true)
        {
            reversecontrols = false;
        }
        if ((reversecontrols) == false)
        {
            float moveX = -Input.GetAxis("Horizontal");
            float moveZ = -Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
        }
        if ((reversecontrols) == true)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
        }
    }

    void Update()
    {
        //Running
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 8; //change speed
            playerCamera.fieldOfView = 100; //change camera view while running (inform player about running through the visuals)
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5;
            playerCamera.fieldOfView = 95.4f;
        }

        //Jumping

        //if player is on the ground (object has Ground layer) - jumps are allowed
        isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + groundCheckDistance, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Mouse Sensitivity
        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
            lookSpeedX = savedSensitivity;
            lookSpeedY = savedSensitivity;
        }

        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        moveAround = MenuObject.GetComponent<Checkpoints>().backToMenuBool; //check if pause menu is open and stop movements if it is
        if (moveAround == false)
        {
            // Rotate player left/right
            transform.Rotate(Vector3.up * mouseX);

            // Rotate player up/down
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}
