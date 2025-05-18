using UnityEngine;

public class Laser_tunnel : MonoBehaviour
{
    public GameObject lasers;
    private float time=1f;

    private void Start()
    {
        Invoke("LasersOn", time);
    }

    void LasersOn()
    {
        lasers.SetActive(true);
        Invoke("LasersOff", time);
    }

    void LasersOff()
    {
        lasers.SetActive(false);
        Invoke("LasersOn", time);
    }
}
