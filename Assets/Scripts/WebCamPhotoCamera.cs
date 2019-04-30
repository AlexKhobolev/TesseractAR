using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class WebCamPhotoCamera : MonoBehaviour
{
    WebCamTexture webCamTexture;
    [SerializeField]
    RawImage rICamera;
    [SerializeField]
    GameObject rILastPhotoHolder;
    Texture2D curPhoto;

    [SerializeField]
    OCRManager ocrManager;

    string path;

    void Start()
    {
        ocrManager = GetComponent<OCRManager>();
        WebCamDevice[] devices = WebCamTexture.devices;
        path = Application.streamingAssetsPath;
        webCamTexture = new WebCamTexture(devices[0].name);
        GetComponent<Renderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();
        print(path + @"/photo.jpg");
    }

    void Update()
    {
        rICamera.texture = webCamTexture;
    }

    public void Photo()
    {
        StartCoroutine(TakePhoto());
    }

    public void ShowPhoto()
    {
        rILastPhotoHolder.GetComponent<RawImage>().texture = curPhoto;
    }

    IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame();
        
        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();
        byte[] bytes = photo.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(path, "photo.png"), bytes);
        ocrManager.GetText(Path.Combine(path, "photo.png"));
        curPhoto = photo;
    }
}