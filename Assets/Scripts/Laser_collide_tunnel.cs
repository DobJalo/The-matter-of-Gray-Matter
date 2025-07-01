using UnityEngine;

public class Laser_collide_tunnel : MonoBehaviour
{
    public GameObject checkpoint; // if player dies, they are being teleported to this object
    public GameObject deathScreen;
    public AudioClip deathSound; // assign in Inspector

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // if player
        {
            deathScreen.SetActive(true); // show death screen

            if (deathSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(deathSound);
            }

            // teleport player back to checkpoint
            other.gameObject.transform.position = new Vector3(
                checkpoint.transform.position.x,
                checkpoint.transform.position.y + 2,
                checkpoint.transform.position.z
            );
        }
    }
}
