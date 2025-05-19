using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public int ballCount = 20;
    public float tunnelRadius = 2f;
    public float tunnelLength = 10f;

    void Start()
    {
        for (int i = 0; i < ballCount; i++)
        {
            // Random position in cylindrical tunnel
            float angle = Random.Range(0f, 360f);
            float radius = Random.Range(0f, tunnelRadius);
            float height = Random.Range(-tunnelLength / 2f, tunnelLength / 2f);

            Vector3 localPos = new Vector3(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius,
                height
            );

            Vector3 worldPos = transform.position + localPos;

            GameObject ball = Instantiate(ballPrefab, worldPos, Quaternion.identity);
        }
    }
}
