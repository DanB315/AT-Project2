using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCycle : MonoBehaviour
{
    [Header("REFERENCES")]
    public Material[] skyMats;

    [Header("VARIABLES")]
    public float time;
    public int mins = 0;
    private float maxMins;
    public float skyboxTime = 15f;

    private void Start()
    {
        maxMins = skyMats.Length;
    }

    private void Update()
    {
        time += Time.deltaTime;
        
        if (time >= skyboxTime)
        {
            mins += 1;
            time = 0;
        }

        if (mins >= maxMins)
        {
            mins = 0;
        }

        RenderSettings.skybox = skyMats[mins];
    }
}
