using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class FileExplorer : MonoBehaviour
{
    string path;
    string savePath;
    string fileName;

    public RawImage rawImage;

    // Start is called before the first frame update
    public void OpenExplorer()
    {
        path = EditorUtility.OpenFilePanel("Image Panel", " ", "png,jpg");
        fileName = Path.GetFileName(path);
        Display();
    }

    // Update is called once per frame
    void Display()
    {
        if (path != null)
        {
            StartCoroutine(GetImage());
        }
    }

    IEnumerator GetImage()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("file:///" + path);
        yield return www.SendWebRequest();
        rawImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        FindObjectOfType<Compositor>()._bgImage = rawImage.texture;
        rawImage.color = Color.white;
    }
}
