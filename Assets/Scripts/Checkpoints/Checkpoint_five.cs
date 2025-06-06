using UnityEngine;

public class Checkpoint_five : MonoBehaviour
{
    public GameObject message;
    private const string CheckpointKey = "Checkpoint";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerPrefs.GetInt("Checkpoint") != 5)
        {
            message.SetActive(true);


            PlayerPrefs.SetInt(CheckpointKey, 5);
            PlayerPrefs.Save();

            Invoke("HideMessage", 5f);
        }
    }

    private void HideMessage()
    {
        message.SetActive(false);
    }
}
