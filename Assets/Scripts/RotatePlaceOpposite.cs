using UnityEngine;

public class RotateInPlaceOpposite : MonoBehaviour
{
    public float speed = - 50f; // degrees per second

    void Update()
    {
        // Rotate counter-clockwise around local X axis
        transform.Rotate(-speed * Time.deltaTime, 0, 0, Space.Self);
    }
}