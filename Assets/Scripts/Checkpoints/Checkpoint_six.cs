using UnityEngine;

public class Checkpoint_six : MonoBehaviour
{
    public GameObject message;
    private const string CheckpointKey = "Checkpoint";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerPrefs.GetInt("Checkpoint") != 6)
        {
            message.SetActive(true);


            PlayerPrefs.SetInt(CheckpointKey, 6);
            PlayerPrefs.Save();

            Invoke("HideMessage", 5f);
        }
    }

    private void HideMessage()
    {
        message.SetActive(false);
    }
}
