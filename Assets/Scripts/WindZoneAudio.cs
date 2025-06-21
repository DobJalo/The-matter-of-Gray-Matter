using UnityEngine;

//audio that will be triggered in the bridge with the wind, should only trigger when entering the wind zone (hopefully)

public class WindZoneAudio : MonoBehaviour
{
    public AudioSource windAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            windAudio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            windAudio.Stop();
        }
    }
}
