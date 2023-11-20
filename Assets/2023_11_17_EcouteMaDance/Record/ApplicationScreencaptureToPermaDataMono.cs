using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ApplicationScreencaptureToPermaDataMono : MonoBehaviour
{
    public int m_superSize = 4;
    

    public void CaptureAndSaveScreenshot()
    {
        // Create a timestamped file name for the screenshot.
        string fileName = "screenshot_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

        // Combine the persistentDataPath with the desired subfolder for the screenshots.
        string subfolderPath = Path.Combine(Application.persistentDataPath, "Screenshots");
        string filePath = Path.Combine(subfolderPath, fileName);

        if (!Directory.Exists(subfolderPath))
        {
            Directory.CreateDirectory(subfolderPath);
        }
        Debug.Log("Screenshot path: " + filePath);

        ScreenCapture.CaptureScreenshot(filePath, m_superSize);
    }
}