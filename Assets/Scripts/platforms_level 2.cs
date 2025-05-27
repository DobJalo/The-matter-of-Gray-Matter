using UnityEngine;

public class platforms_level2 : MonoBehaviour
{
    public GameObject pl1;
    public GameObject pl2;
    public GameObject pl3;
    public GameObject pl4;
    public GameObject pl5;
    public GameObject pl6;
    public GameObject pl7;
    public GameObject pl8;
    public GameObject pl9;
    public GameObject pl10;
    public GameObject pl11;
    public GameObject pl12;
    public GameObject pl13;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(this.gameObject == pl9)
            {
                other.gameObject.transform.position = new Vector3(pl4.transform.position.x, pl4.transform.position.y, pl4.transform.position.z);
            }
            if (this.gameObject == pl8)
            {
                other.gameObject.transform.position = new Vector3(pl6.transform.position.x, pl6.transform.position.y, pl6.transform.position.z);
            }
            if (this.gameObject == pl5)
            {
                other.gameObject.transform.position = new Vector3(pl3.transform.position.x, pl3.transform.position.y, pl3.transform.position.z);
            }
            if (this.gameObject == pl2)
            {
                other.gameObject.transform.position = new Vector3(pl7.transform.position.x, pl7.transform.position.y, pl7.transform.position.z);
            }
            if (this.gameObject == pl11)
            {
                other.gameObject.transform.position = new Vector3(pl10.transform.position.x, pl10.transform.position.y, pl10.transform.position.z);
            }

        }
    }
}

