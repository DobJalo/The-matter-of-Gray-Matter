using UnityEngine;

public class Checkpoint_two : MonoBehaviour
{
    public GameObject message;
    private const string CheckpointKey = "Checkpoint"; //save slot

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerPrefs.GetInt("Checkpoint") != 2)
        {
            message.SetActive(true);


            PlayerPrefs.SetInt(CheckpointKey, 2);
            PlayerPrefs.Save();

            Invoke("HideMessage", 5f);
        }
    }

    private void HideMessage()
    {
        message.SetActive(false);
        //this.gameObject.SetActive(false);
    }
}
