using UnityEngine;

public class Wind : MonoBehaviour
{
    public float pushForce = 7f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();

            if (playerRb != null)
            {

                Vector3 playerDirection = other.transform.forward;

               
                float dotProduct = Vector3.Dot(playerDirection, Vector3.forward); 

                if (dotProduct > 0) 
                {
                
                    Vector3 leftPush = -other.transform.right * pushForce;
                    playerRb.linearVelocity = new Vector3(leftPush.x, playerRb.linearVelocity.y, leftPush.z);
                }
                else if (dotProduct < 0) 
                {

                    Vector3 rightPush = other.transform.right * pushForce;
                    playerRb.linearVelocity = new Vector3(rightPush.x, playerRb.linearVelocity.y, rightPush.z); 
                }
            }
        }
    }
}
