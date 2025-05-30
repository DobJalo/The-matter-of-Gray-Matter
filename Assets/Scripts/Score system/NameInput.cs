using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this scripts lets player to enter their name, then it saves the name

public class NameInput : MonoBehaviour
{
    public InputField nameInput;
    string input1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // check if Enter button is pressed
        {
            input1 = nameInput.text.Trim(); // get text from input value
            if (string.IsNullOrEmpty(input1)) return;// does not let player to leave empty space in name field
            PlayerName.playerName = input1; //save player name
            PlayerPrefs.DeleteKey("Checkpoint"); // delete checkpoints info bacuse it is a new player
            SceneManager.LoadScene("SampleScene"); //load next scene
          
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu"); //load main menu scene
    }
}
