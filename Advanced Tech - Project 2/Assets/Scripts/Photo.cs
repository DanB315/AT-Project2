using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Photo : MonoBehaviour
{
    [Header("REFERENCES")]
    public Interact interact;
    public GameObject interactCanvas;

    [Header("PHOTOTAKER")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject CameraCanvas;
    public GameObject textObject;
    public TextMeshProUGUI photoText;
    public bool cameraReady = false;
    public bool photoOfImportance = false;

    [Header("CAMERA FLASH")]
    [SerializeField] private GameObject camFlash;
    [SerializeField] private float flashTime;

    private Texture2D screenCap;
    private bool viewingPhoto;

    private void Start()
    {
        screenCap = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        interact = GameObject.Find("Player").GetComponent<Interact>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CameraCanvas.SetActive(true);
            cameraReady = true;
            RemovePhoto();
        }

        else if (Input.GetMouseButtonUp(1))
        {
            CameraCanvas.SetActive(false);
            cameraReady = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (cameraReady)
            {
                if (!viewingPhoto)
                {
                    StartCoroutine(CapturePhoto());
                    if (interact.lookingAt)
                    {
                        photoOfImportance = true;
                    }

                    else
                    {
                        photoOfImportance = false;
                    }
                }
            }
            else
            {
                RemovePhoto();
                photoOfImportance = false;
            }
        }

        if (viewingPhoto && photoOfImportance)
        {
            photoText.SetText(interact.objLook.name + " was photographed!");
            photoText.enabled = true;
        }

        else
        {
            photoText.enabled = false;
        }
    }

    IEnumerator CapturePhoto()
    {
        CameraCanvas.SetActive(false);
        interactCanvas.SetActive(false);
        photoText.enabled = false;
        viewingPhoto = true;
        yield return new WaitForEndOfFrame();
        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        screenCap.ReadPixels(regionToRead, 0, 0, false);
        screenCap.Apply();
        ShowPhoto();
    }

    void ShowPhoto()
    {
        StartCoroutine(DoFlash());
        Sprite photoSprite = Sprite.Create(screenCap, new Rect(0.0f, 0.0f, screenCap.width, screenCap.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;
        photoFrame.SetActive(true);
    }

    IEnumerator DoFlash()
    {
        camFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        camFlash.SetActive(false);
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        interactCanvas.SetActive(true);
    }
}
