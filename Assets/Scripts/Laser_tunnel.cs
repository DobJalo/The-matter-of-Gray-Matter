using UnityEngine;

public class Laser_tunnel : MonoBehaviour
{
    public GameObject lasers;
    public AudioClip laserOnSound;           // Drag your laser sound clip here in the Inspector
    private AudioSource audioSource;
    private float time = 1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        Invoke("LasersOn", time);
    }

    void LasersOn()
    {
        lasers.SetActive(true);

        if (laserOnSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(laserOnSound);
        }

        Invoke("LasersOff", time);
    }

    void LasersOff()
    {
        lasers.SetActive(false);
        Invoke("LasersOn", time);
    }
}
