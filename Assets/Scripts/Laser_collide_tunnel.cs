using Unity.VisualScripting;
using UnityEngine;

public class Laser_collide_tunnel : MonoBehaviour
{
    public GameObject checkpoint; // if player dies, they are being teleported to this object
    public GameObject deathScreen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // if player
        {
            deathScreen.SetActive(true); // show death screen

            //teleport player back to checkpoint
            other.gameObject.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y + 2, checkpoint.transform.position.z); 
        }
    }
}
