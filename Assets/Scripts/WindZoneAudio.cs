using UnityEngine;

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
