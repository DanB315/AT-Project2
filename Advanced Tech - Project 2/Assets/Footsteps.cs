using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [Header("REFERENCES")]
    FPSController fpc;
    AudioSource footsteps;
    public AudioClip grassFoot, mudFoot, waterFoot;

    [Header("VARIABLES")]
    public string currentGround;

    private void Start()
    {
        fpc = GetComponent<FPSController>();
        footsteps = GameObject.Find("FootstepsAudio").GetComponent<AudioSource>();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            currentGround = hit.collider.gameObject.tag;
        }

        if (fpc.isMoving)
        {
            footsteps.Play();
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

        else
        {
            footsteps.Stop();
        }
    }
}
