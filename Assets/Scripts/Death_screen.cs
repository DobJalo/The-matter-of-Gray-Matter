using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Death_screen : MonoBehaviour
{
    private KeyCode pressedKey;

    public GameObject player;
    private Rigidbody playerRb;

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        /*if(Input.anyKey) // press any key
        {
            this.gameObject.SetActive(false); // hide this object - death screen
        }*/

        playerRb.linearVelocity = Vector3.zero;


        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                pressedKey = key;
            }

            if (Input.GetKeyUp(key) && key == pressedKey)
            {
                this.gameObject.SetActive(false);
                pressedKey = KeyCode.None; 
            }
        }
    }
}
