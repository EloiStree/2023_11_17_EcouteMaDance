using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ApplicationRenderTextureToPermaDataMono : MonoBehaviour
{
    public RenderTexture m_renderTexture;
    public Save m_save;
    public enum Save { SaveTime, SaveRelativeTime }


    [ContextMenu("Screenshot")]
    public void Screenshot()
    {
        string fileName = "screenshot_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        string subfolderPath = Path.Combine(Application.persistentDataPath, "Screenshots");
        string filePath = Path.Combine(subfolderPath, fileName);

        if (!Directory.Exists(subfolderPath))
        {
            Directory.CreateDirectory(subfolderPath);
        }
        Debug.Log("Screenshot path: " + filePath);

        // Create a texture 2D and read the pixels from the RenderTexture
        Texture2D screenshot = new Texture2D(m_renderTexture.width, m_renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = m_renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, m_renderTexture.width, m_renderTexture.height), 0, 0);
        RenderTexture.active = null;
        byte[] bytes = screenshot.EncodeToPNG();
        System.IO.File.WriteAllBytes(filePath, bytes);
    }
}
