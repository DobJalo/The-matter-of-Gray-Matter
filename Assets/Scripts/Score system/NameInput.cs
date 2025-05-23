using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    public InputField nameInput;
    string input1;

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Return))
        {
            input1 = nameInput.text.Trim();
            if (string.IsNullOrEmpty(input1)) return;// does not let player to leave empty space
            PlayerName.playerName = input1; //save player name
            SceneManager.LoadScene("SampleScene");
          
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
