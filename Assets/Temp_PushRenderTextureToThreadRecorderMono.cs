using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_PushRenderTextureToThreadRecorderMono : MonoBehaviour
{

    public ThreadRecorderRenderTextureMono m_threadRecorder;
    public RenderTexture m_source;
    public Texture2D m_tempTexture;

    [ContextMenu("Save And Push To Thread ")]
    public void SaveAndPushToThread() {
        m_tempTexture = new Texture2D(m_source.width, m_source.height, TextureFormat.RGB24, false);
        RenderTexture.active = m_source;
        m_tempTexture.ReadPixels(new Rect(0, 0, m_source.width, m_source.height), 0, 0);

        RenderTexture.active = null;
        m_threadRecorder.Add(m_tempTexture);
        
    }

}
