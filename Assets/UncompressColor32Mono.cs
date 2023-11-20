using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class UncompressColor32Mono : MonoBehaviour
{
    public string m_path;
    public int m_width=2048;
    public int m_height = 2048;
    public int m_index;
    [ContextMenu("Uncompress")]
    public void Uncompress() {
        StartCoroutine(Coroutine_Uncompress());
    }

    IEnumerator Coroutine_Uncompress()
    {

        Texture2D tx = new Texture2D(m_width, m_height);
      string [] files =  Directory.GetFiles(m_path, "*.color32", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            m_index= i;
            byte[] f = File.ReadAllBytes(files[i]);
            Color32[] c =  ConvertToColor32(f);
            string t = files[i] + ".png";
            tx.SetPixels32(c);
            tx.Apply();
            f= tx.EncodeToPNG();
            File.WriteAllBytes(t, f);
            yield return new WaitForEndOfFrame();

        }


    }
    public Color32[] ConvertToColor32(byte[] value) { return MemoryMarshal.Cast<byte, Color32>(value).ToArray(); }


    // Update is called once per frame
    void Update()
    {
        
    }
}
