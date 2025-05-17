using UnityEngine;

public class RotateOnPlace : MonoBehaviour
{
    public float speed = - 50f; // degrees per second

    void Update()
    {
        // Rotate clockwise around local X axis
        transform.Rotate(speed * Time.deltaTime, 0, 0, Space.Self);
    }
}
