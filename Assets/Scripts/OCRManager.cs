using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tesseract;
using UnityEngine;
using UnityEngine.UI;

public class OCRManager : MonoBehaviour
{    
    [SerializeField]
    public Text resultText;
    string dataPath;

    void Start()
    {
        dataPath = Path.Combine(Application.streamingAssetsPath, "tessdata");
    }

    public string GetText(string path)
    {
        var ocr = new TesseractEngine(dataPath, "rus", EngineMode.Default);
        Pix p = Pix.LoadFromFile(path);
        var page = ocr.Process(p);
        var result = page.GetText().Split(' ', '\n')[0];
        resultText.text = result;
        return result;
    }
}
