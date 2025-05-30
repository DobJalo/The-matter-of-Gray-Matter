using UnityEngine;

public class Checkpoint_three : MonoBehaviour
{
    public GameObject message;
    private const string CheckpointKey = "Checkpoint";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerPrefs.GetInt("Checkpoint") != 3)
        {
            message.SetActive(true);


            PlayerPrefs.SetInt(CheckpointKey, 3);
            PlayerPrefs.Save();

            Invoke("HideMessage", 5f);
        }
    }

    private void HideMessage()
    {
        message.SetActive(false);
    }
}
