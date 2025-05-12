using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.2f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;

    public Transform cameraTransform;
    private Rigidbody rb;
    private float xRotation = 0f;

    public GameObject plane2;
    public GameObject plane3;

    public GameObject MenuObject;
    private bool moveAround = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get Rigidbody component from Player

        //start position (checkpoints)
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            if (PlayerPrefs.GetInt("Checkpoint") == 1)
            {
                this.gameObject.transform.position = plane2.transform.position;
            }
            if (PlayerPrefs.GetInt("Checkpoint") == 2)
            {
                this.gameObject.transform.position = plane3.transform.position;
            }
        }

         
    }

    void FixedUpdate() // FixedUpdate is better for synchronization physics and movement
    {
        // Player movement
        float moveX = -Input.GetAxis("Horizontal");
        float moveZ = -Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
    }

    void Update()
    {
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

        moveAround = MenuObject.GetComponent<Checkpoints>().backToMenuBool;
        if (moveAround==false)
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
