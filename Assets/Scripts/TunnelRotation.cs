using UnityEngine;

public class TunnelRotation : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, -50f, 0);

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
