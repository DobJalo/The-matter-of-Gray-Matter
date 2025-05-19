using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public int ballCount = 20;
    public float tunnelRadius = 2f;
    public float tunnelLength = 10f;
    public float moveSpeed = 2f;

    void Start()
    {
        for (int i = 0; i < ballCount; i++)
        {
            // Random radial placement
            float angle = Random.Range(0f, 360f);
            float radius = Random.Range(0f, tunnelRadius);
            float height = Random.Range(-tunnelLength / 2f, tunnelLength / 2f);

            Vector3 localPos = new Vector3(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius,
                height
            );

            GameObject ball = Instantiate(ballPrefab, transform.position + localPos, Quaternion.identity);

            // Add movement and scale control
            BallMover mover = ball.AddComponent<BallMover>();
            mover.tunnelRadius = tunnelRadius;
            mover.moveSpeed = moveSpeed;
        }
    }
}

