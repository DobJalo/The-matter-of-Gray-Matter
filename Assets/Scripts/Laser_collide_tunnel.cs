using Unity.VisualScripting;
using UnityEngine;

public class Laser_collide_tunnel : MonoBehaviour
{
    public GameObject checkpoint;
    public GameObject deathScreen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            deathScreen.SetActive(true);
            other.gameObject.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y + 2, checkpoint.transform.position.z); 
        }
    }
}
