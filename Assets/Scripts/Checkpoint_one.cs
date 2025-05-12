using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Checkpoint_one : MonoBehaviour
{
    public GameObject message;
    private const string CheckpointKey = "Checkpoint"; //save slot

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && PlayerPrefs.GetInt("Checkpoint") != 1)
        {
            message.SetActive(true);


            PlayerPrefs.SetInt(CheckpointKey, 1);
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

// 0 - start position
// 1 - checkpoint 1
// 2 - checkpoint 2
// 3 - end checkpoint (not needed?)