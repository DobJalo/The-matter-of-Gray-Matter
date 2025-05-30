using UnityEngine;

public class platforms_level2 : MonoBehaviour
{
    public GameObject pl1_1;
    public GameObject pl1_2;

    public GameObject pl2_1;
    public GameObject pl2_2;

    public GameObject pl3_1;
    public GameObject pl3_2;

    public GameObject pl4_1;
    public GameObject pl4_2;

    private int dist = 2;



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(this.gameObject == pl1_1)
            {
                other.gameObject.transform.position = new Vector3(pl1_2.transform.position.x, pl1_2.transform.position.y + dist, pl1_2.transform.position.z);
            }
            if (this.gameObject == pl2_1)
            {
                other.gameObject.transform.position = new Vector3(pl2_2.transform.position.x, pl2_2.transform.position.y + dist, pl2_2.transform.position.z);
            }
            if (this.gameObject == pl3_1)
            {
                other.gameObject.transform.position = new Vector3(pl3_2.transform.position.x, pl3_2.transform.position.y + dist, pl3_2.transform.position.z);
            }
            if (this.gameObject == pl4_1)
            {
                other.gameObject.transform.position = new Vector3(pl4_2.transform.position.x, pl4_2.transform.position.y + dist, pl4_2.transform.position.z);
            }

        }
    }
}

