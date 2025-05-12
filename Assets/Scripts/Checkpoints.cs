using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{
    public GameObject player;
    public GameObject plane2;
    public GameObject plane3;
    public GameObject backToMenu;

    public bool backToMenuBool = false;

    // Update is called once per frame
    void Update()
    {
        //switch between levels
        if (Input.GetKey("0") && Input.GetKey(KeyCode.LeftShift))
        {
            player.transform.position = new Vector3(0, 0, 0); //start position
            PlayerPrefs.DeleteKey("Checkpoint");
        }
        if (Input.GetKey("1") && Input.GetKey(KeyCode.LeftShift))
        {
            player.transform.position = plane2.transform.position;
        }
        if (Input.GetKey("2") && Input.GetKey(KeyCode.LeftShift))
        {
            player.transform.position = plane3.transform.position;
        }
        if (Input.GetKey("3") && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("End checkpoint in Progress");
        }
        if (Input.GetKey("4") && Input.GetKey(KeyCode.LeftShift))
        {
            PlayerPrefs.DeleteKey("Checkpoint");
            Debug.Log("Checkpoints were deleted");
        }

        //pause
        if (Input.GetKeyDown(KeyCode.Escape) && backToMenuBool==false)
        {
            backToMenu.SetActive(true);
            backToMenuBool = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && backToMenuBool == true)
        {
            backToMenu.SetActive(false);
            backToMenuBool = false;
        }
    }

    public void CloseMenu()
    {
        backToMenu.SetActive(false);
        backToMenuBool = false;
    }

    //to main menu
    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    //if player falls
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!PlayerPrefs.HasKey("Checkpoint"))
            {
                other.gameObject.transform.position = new Vector3(0, 0, 0);
            }
            if (PlayerPrefs.GetInt("Checkpoint")==1)
            {
                other.gameObject.transform.position = plane2.transform.position;
            }
            if (PlayerPrefs.GetInt("Checkpoint") == 2)
            {
                other.gameObject.transform.position = plane3.transform.position;
                
            }
        }
    }
}
