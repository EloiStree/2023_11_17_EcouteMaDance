using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Temp_CheckIfAiDidGoodMono : MonoBehaviour
{


    public List<EventToTexture> m_eventToColor = new();

    public TextureEvent m_onPositionFoundTexture;
    public StringEvent m_onPositionFoundLabel;
    [System.Serializable]
    public class TextureEvent : UnityEvent<Texture2D> { }
    [System.Serializable]
    public class StringEvent : UnityEvent<string> { }

    [System.Serializable]
    public class EventToTexture
    {

        public string m_eventName;
        public Texture2D m_wantedTexture;

    }

    

    public void PushEvent(string text)
    {
        EventToTexture[] v = m_eventToColor.Where(k => k.m_eventName.Trim().ToLower() == text.Trim().ToLower()).ToArray();
        if (v.Length > 0)
        {
            foreach (var vc in v)
            {
                m_onPositionFoundLabel.Invoke(vc.m_eventName);
                m_onPositionFoundTexture.Invoke(vc.m_wantedTexture);
            }
        }

    }
    
}
