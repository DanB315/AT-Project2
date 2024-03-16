using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [Header("REFERENCES")]
    FPSController fpc;
    AudioSource footsteps;
    public AudioClip grassFoot, mudFoot, waterFoot;
    CharacterController charController;

    [Header("VARIABLES")]
    public string currentGround;

    private void Start()
    {
        fpc = GetComponent<FPSController>();
        footsteps = GameObject.Find("FootstepsAudio").GetComponent<AudioSource>();
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            currentGround = hit.collider.gameObject.tag;
        }

        if (fpc.isMoving && charController.isGrounded)
        {
            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
        }

        else
        {
            footsteps.Stop();
        }

        if (fpc.isSprinting)
        {
            footsteps.pitch = 1.5f;
        }

        else
        {
            footsteps.pitch = 1f;
        }


        if (currentGround == "Grass")
        {
            footsteps.clip = grassFoot;
        }

        else if (currentGround == "Mud")
        {
            footsteps.clip = mudFoot;
        }

        else if (currentGround == "Water")
        {
            footsteps.clip = waterFoot;
        }
    }
}
