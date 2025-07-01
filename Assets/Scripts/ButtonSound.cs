using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;

            if (selected != null && selected.GetComponent<Button>())
            {
                // Play the sound
                audioSource.PlayOneShot(clickSound);

                // Trigger the button click event
                selected.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}

