using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log(PlayerName.playerName);
        if (PlayerName.playerName == "")
        {
            // Load next scene (make sure it's added to Build Settings)
            SceneManager.LoadScene("EnterYourName");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
