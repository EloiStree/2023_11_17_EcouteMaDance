using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UtilitySaveAndLoadRenderTexture : MonoBehaviour
{

    public RenderTexture m_renderTexture;

    Texture2D m_tempTexture = null;
    public bool m_useVerticalFlip;
    public byte[] CaptureFrame()
    {
        if (m_renderTexture.width != m_tempTexture.width || m_renderTexture.height != m_tempTexture.height) { 
            m_tempTexture = new Texture2D(m_renderTexture.width, m_renderTexture.height, TextureFormat.RGBA32, false);
        }
        RenderTexture.active = m_renderTexture;
        if(m_useVerticalFlip)
            VerticallyFlipRenderTexture(m_renderTexture);
        m_tempTexture.ReadPixels(new Rect(0, 0, m_renderTexture.width, m_renderTexture.height), 0, 0);
        RenderTexture.active = null;

        byte[] bArray = m_tempTexture.GetRawTextureData();
        return bArray;
    }
    public bool m_mip=true;
    public bool m_linear=true;
    public void LoadFrameAsTexture2D(int width, int height, byte[] value, out Texture2D texture)
    {
        texture = new Texture2D(width, height, TextureFormat.RGBA32, m_mip, m_linear);
        texture.LoadRawTextureData(value);
    }
    public void LoadFrameAsPNG(int width, int height, byte[] value, string path)
    {
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, m_mip, m_linear);
        texture.LoadRawTextureData(value);
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllBytes(path,texture.EncodeToPNG());
    }

    private static void VerticallyFlipRenderTexture(RenderTexture target)
    {
        var temp = RenderTexture.GetTemporary(target.descriptor);
        Graphics.Blit(target, temp, new Vector2(1, -1), new Vector2(0, 1));
        Graphics.Blit(temp, target);
        RenderTexture.ReleaseTemporary(temp);
    }

}
