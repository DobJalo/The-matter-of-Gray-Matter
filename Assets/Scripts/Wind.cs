using UnityEngine;

public class Wind : MonoBehaviour
{
    public float pushForce = 7f;
    public Vector3 windDirection = Vector3.left;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                Vector3 push = windDirection.normalized * pushForce;

                playerRb.linearVelocity = new Vector3(push.x, playerRb.linearVelocity.y, push.z);
            }
        }
    }
}
