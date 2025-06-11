using UnityEngine;

//Footstep sounds will be triggered whenever the player is pressing one of the walking buttons (W,A,S,D) ((please please please work))

public class footsteps : MonoBehaviour

{

    public AudioSource footstepsSound, sprintSound;



    void Update()

    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {

            if (Input.GetKey(KeyCode.LeftShift))

            {

                footstepsSound.enabled = false;

                sprintSound.enabled = true;

            }

            else

            {

                footstepsSound.enabled = true;

                sprintSound.enabled = false;

            }

        }

        else

        {

            footstepsSound.enabled = false;

            sprintSound.enabled = false;

        }

    }

}