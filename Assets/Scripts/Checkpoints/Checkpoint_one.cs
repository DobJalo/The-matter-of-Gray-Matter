using UnityEngine;
public class Checkpoint_one : MonoBehaviour
{
    public GameObject message;
    private const string CheckpointKey = "Checkpoint"; //save slot

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && PlayerPrefs.GetInt("Checkpoint") != 1) //if tag is "player" and info about current checkpoint have not been saved before
        {
            message.SetActive(true); //show UI "checkpoint reached"

            //save information that this checkpoint has been reached
            PlayerPrefs.SetInt(CheckpointKey, 1);
            PlayerPrefs.Save();

            // hide UI message in 5 seconds
            Invoke("HideMessage", 5f);
        }
    }
    private void HideMessage()
    {
        message.SetActive(false); //hide message
    }
}
// CheckpointKey values:
// 0 - start position
// 1 - checkpoint 1
// 2 - checkpoint 2
// 3 - end checkpoint (not needed?)