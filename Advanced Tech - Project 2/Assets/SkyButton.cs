using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyButton : MonoBehaviour
{
    [Header("REFERENCES")]
    public Material[] skyMats;
    Interact interact;
    GameObject player;

    [Header("INTERACTIBLES")]
    public int i = 0;

    void Start()
    {
        player = GameObject.Find("Player");
        interact = player.GetComponent<Interact>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interact.objInteracted)
        {
            interact.objInteracted = false;
            i += 1;
        }

        if (i >= skyMats.Length)
        {
            i = 0;
        }

        RenderSettings.skybox = skyMats[i];
    }
}
