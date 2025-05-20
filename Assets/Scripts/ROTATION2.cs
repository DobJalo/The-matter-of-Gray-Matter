using UnityEngine;

public class RotateTunnel : MonoBehaviour
{
    public float speed = 30f; // Speed in degrees per second

    void Update()
    {
        // Rotate clockwise on the X-axis
        transform.Rotate(speed * Time.deltaTime, 0, 0, Space.Self);
    }
}


