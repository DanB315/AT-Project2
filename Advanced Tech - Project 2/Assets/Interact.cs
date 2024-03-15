using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    [Header("REFERENCES")]
    public Camera playerCam;
    public TextMeshProUGUI interactText;

    [Header("INTERACT")]
    public GameObject objLook;
    public bool lookingAt;
    public bool objInteracted;
    public LayerMask interactLayer;
    public float interactDis = 3f;

    private void Start()
    {
        playerCam = Camera.main;
    }

    private void Update()
    {
        RaycastHit hitData;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward), out hitData, interactDis, interactLayer))
        {
            objLook = hitData.transform.gameObject;
            lookingAt = true;
        }

        else
        {
            objLook = null;
            lookingAt = false;
        }


        if (lookingAt && !objInteracted)
        {
            interactText.enabled = true;
        }

        else
        {
            interactText.enabled = false;
        }


        if (lookingAt && Input.GetButtonDown("Interact") && !objInteracted)
        {
            print(objLook.gameObject.name + " was interacted with.");
            objInteracted = true;
        }
    }
}
