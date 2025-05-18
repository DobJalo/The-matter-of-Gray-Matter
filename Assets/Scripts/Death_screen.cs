using UnityEngine;

public class Death_screen : MonoBehaviour
{
    private void Update()
    {
        if(Input.anyKey)
        {
            this.gameObject.SetActive(false);
        }
    }
}
