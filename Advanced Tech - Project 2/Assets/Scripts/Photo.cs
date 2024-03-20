using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour
{
    [Header("PHOTOTAKER")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject CameraCanvas;
    public bool cameraReady = false;

    private Texture2D screenCap;
    private bool viewingPhoto;

    private void Start()
    {
        screenCap = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cameraReady)
        {
            if (!viewingPhoto)
            {
                StartCoroutine(CapturePhoto());
            }
            else
            {
                RemovePhoto();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            CameraCanvas.SetActive(true);
            cameraReady = true;
            RemovePhoto();
        }

        if (Input.GetMouseButtonUp(1))
        {
            CameraCanvas.SetActive(false);
            cameraReady = false;
        }
    }

    IEnumerator CapturePhoto()
    {
        CameraCanvas.SetActive(false);
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        screenCap.ReadPixels(regionToRead, 0, 0, false);
        screenCap.Apply();
        ShowPhoto();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCap, new Rect(0.0f, 0.0f, screenCap.width, screenCap.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;

        photoFrame.SetActive(true);
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
    }
}
