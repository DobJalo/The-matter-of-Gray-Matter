using UnityEngine;

public class Laser_tunnel : MonoBehaviour
{
    public GameObject lasers;
    private float time=1f;

    private void Start()
    {
        Invoke("LasersOn", time); // starts LaserOn in "time" seconds (float "time" is 1 second)
    }

    void LasersOn()
    {
        lasers.SetActive(true); // show lasers
        Invoke("LasersOff", time); // starts LasersOff in "time" seconds (float "time" is 1 second)
    }

    void LasersOff()
    {
        lasers.SetActive(false); // hide lasers
        Invoke("LasersOn", time); // starts LaserOn in "time" seconds (float "time" is 1 second)
    }

    //the process repeats all the time during the game
}
