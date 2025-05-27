using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{
    public GameObject player;
    private Rigidbody playerRB;
    public GameObject plane2;
    public GameObject plane3;
    public GameObject ending;
    public GameObject backToMenu;
    public GameObject deathScreen;

    public bool backToMenuBool = false;

    private int distance = 3;

    private void Start()
    {
        playerRB = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //quick switch between checkpoints
        if (Input.GetKey("1") && Input.GetKey(KeyCode.LeftShift)) //if "1" and Shift are pressed at the same time
        {
            playerRB.linearVelocity = Vector3.zero;
            player.transform.position = new Vector3(0, distance, 0); //teleport player to start position
            PlayerPrefs.DeleteKey("Checkpoint"); //delete any information about saved checkpoints
        }
        //same logic below:
        if (Input.GetKey("2") && Input.GetKey(KeyCode.LeftShift))
        {
            playerRB.linearVelocity = Vector3.zero;
            player.transform.position = new Vector3(plane2.transform.position.x, plane2.transform.position.y + distance, plane2.transform.position.z);
        }
        if (Input.GetKey("3") && Input.GetKey(KeyCode.LeftShift))
        {
            playerRB.linearVelocity = Vector3.zero;
            player.transform.position = new Vector3(plane3.transform.position.x, plane3.transform.position.y + distance, plane3.transform.position.z);
        }
        if (Input.GetKey("4") && Input.GetKey(KeyCode.LeftShift))
        {
            playerRB.linearVelocity = Vector3.zero;
            player.transform.position = new Vector3(ending.transform.position.x, ending.transform.position.y + distance, ending.transform.position.z);
        }
        //deletes the information about checkpoint saves
        if (Input.GetKey("5") && Input.GetKey(KeyCode.LeftShift))
        {
            playerRB.linearVelocity = Vector3.zero;
            PlayerPrefs.DeleteKey("Checkpoint");
            Debug.Log("Checkpoints were deleted");
        }
        


        //pause menu (opens on pressing Escape)
        if (Input.GetKeyDown(KeyCode.Escape) && backToMenuBool==false)
        {
            backToMenu.SetActive(true); //show pause menu
            backToMenuBool = true; //this variable helps to prevent pause menu opening and closing all the time when Escape is pressed 
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && backToMenuBool == true)
        {
            backToMenu.SetActive(false); //hide pause menu
            backToMenuBool = false;
        }
    }

 
    public void CloseMenu() //close pause menu
    {
        backToMenu.SetActive(false);
        backToMenuBool = false;
    }
    public void BackToMenu() //to main menu
    {
        SceneManager.LoadScene("Main Menu"); // load main menu scene
    }
    private void OnTriggerEnter(Collider other) //if player falls out of the level (death)
    {
        if(other.CompareTag("Player"))
        {
            deathScreen.SetActive(true); //show death screen

            //teleport player based on the information about last checkpoint
            if (!PlayerPrefs.HasKey("Checkpoint"))
            {
                playerRB.linearVelocity = Vector3.zero;
                other.gameObject.transform.position = new Vector3(0, distance+1, 0);
            }
            if (PlayerPrefs.GetInt("Checkpoint")==1)
            {
                playerRB.linearVelocity = Vector3.zero;
                other.gameObject.transform.position = new Vector3(plane2.transform.position.x, plane2.transform.position.y+ distance, plane2.transform.position.z);
            }
            if (PlayerPrefs.GetInt("Checkpoint") == 2)
            {
                playerRB.linearVelocity = Vector3.zero;
                other.gameObject.transform.position = new Vector3(plane3.transform.position.x, plane3.transform.position.y + distance, plane3.transform.position.z);

            }
        }
    }
}
