using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{
    public GameObject player;
    public GameObject plane1;
    public GameObject plane2;
    public GameObject plane3;
    public GameObject plane4;
    public GameObject plane5;
    public GameObject plane6;
    public GameObject ending;
    public GameObject backToMenu;
    public GameObject deathScreen;

    public bool backToMenuBool = false;

    private void Start()
    {
        HideCursor();
    }

    void Update()
    {
        //quick switch between checkpoints
        if (Input.GetKey("1") && Input.GetKey(KeyCode.LeftShift)) //if "1" and Shift are pressed at the same time
        {
            player.transform.position = new Vector3(plane1.transform.position.x, plane1.transform.position.y + 2, plane1.transform.position.z); //teleport player to start position
            PlayerPrefs.DeleteKey("Checkpoint"); //delete any information about saved checkpoints
        }
        //same logic below:
        if (Input.GetKey("2") && Input.GetKey(KeyCode.LeftShift))
        {
            player.transform.position = new Vector3(plane2.transform.position.x, plane2.transform.position.y + 2, plane2.transform.position.z);
        }
        if (Input.GetKey("3") && Input.GetKey(KeyCode.LeftShift))
        {
            player.transform.position = new Vector3(plane3.transform.position.x, plane3.transform.position.y + 2, plane3.transform.position.z);
        }
        if (Input.GetKey("4") && Input.GetKey(KeyCode.LeftShift))
        {
            player.transform.position = new Vector3(plane4.transform.position.x, plane4.transform.position.y + 2, plane4.transform.position.z);
        }
        if (Input.GetKey("5") && Input.GetKey(KeyCode.LeftShift))
        {
            player.transform.position = new Vector3(plane5.transform.position.x, plane5.transform.position.y + 2, plane5.transform.position.z);
        }
        if (Input.GetKey("6") && Input.GetKey(KeyCode.LeftShift))
        {
            player.transform.position = new Vector3(plane6.transform.position.x, plane6.transform.position.y + 2, plane6.transform.position.z);
        }
        if (Input.GetKey("7") && Input.GetKey(KeyCode.LeftShift))
        {
            player.transform.position = new Vector3(ending.transform.position.x, ending.transform.position.y + 2, ending.transform.position.z);
        }
        //deletes the information about checkpoint saves
        if (Input.GetKey("8") && Input.GetKey(KeyCode.LeftShift))
        {
            PlayerPrefs.DeleteKey("Checkpoint");
            Debug.Log("Checkpoints were deleted");
        }
        


        //pause menu (opens on pressing Escape)
        if (Input.GetKeyDown(KeyCode.Escape) && backToMenuBool==false)
        {
            backToMenu.SetActive(true); //show pause menu
            backToMenuBool = true; //this variable helps to prevent pause menu opening and closing all the time when Escape is pressed 
            ShowCursor();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && backToMenuBool == true)
        {
            HideCursor();
            backToMenu.SetActive(false); //hide pause menu
            backToMenuBool = false;
        }
    }

 
    public void CloseMenu() //close pause menu
    {
        backToMenu.SetActive(false);
        backToMenuBool = false;
        HideCursor();
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
                other.gameObject.transform.position = new Vector3(plane1.transform.position.x, plane1.transform.position.y + 2, plane1.transform.position.z);
            }
            if (PlayerPrefs.GetInt("Checkpoint")==1)
            {
                other.gameObject.transform.position = new Vector3(plane2.transform.position.x, plane2.transform.position.y+2, plane2.transform.position.z);
            }
            if (PlayerPrefs.GetInt("Checkpoint") == 2)
            {
                other.gameObject.transform.position = new Vector3(plane3.transform.position.x, plane3.transform.position.y + 3, plane3.transform.position.z);

            }
            if (PlayerPrefs.GetInt("Checkpoint") == 3)
            {
                other.gameObject.transform.position = new Vector3(plane4.transform.position.x, plane4.transform.position.y + 3, plane4.transform.position.z);

            }
            if (PlayerPrefs.GetInt("Checkpoint") == 4)
            {
                other.gameObject.transform.position = new Vector3(plane5.transform.position.x, plane5.transform.position.y + 3, plane5.transform.position.z);

            }
            if (PlayerPrefs.GetInt("Checkpoint") == 5)
            {
                other.gameObject.transform.position = new Vector3(plane6.transform.position.x, plane6.transform.position.y + 3, plane6.transform.position.z);

            }
            if (PlayerPrefs.GetInt("Checkpoint") == 6)
            {
                other.gameObject.transform.position = new Vector3(ending.transform.position.x, ending.transform.position.y + 3, ending.transform.position.z);

            }
        }
    }


    void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
