using UnityEngine;

public class BallMover : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float tunnelRadius = 2f;
    private Vector3 center;

    void Start()
    {
        // Assuming the tunnel is centered at (0,0)
        center = new Vector3(0, 0, transform.position.z); // Use Z from current
    }

    void Update()
    {
        // Move the ball forward (e.g., along Z-axis)
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

        // Get distance from center of tunnel (in XY plane)
        float distFromCenter = Vector2.Distance(
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(center.x, center.y)
        );

        // Calculate scale based on distance from center
        float scale = Mathf.Lerp(0.5f, 2f, distFromCenter / tunnelRadius);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
