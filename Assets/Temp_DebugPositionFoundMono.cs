using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Temp_DebugPositionFoundMono : MonoBehaviour
{

    public Material m_userShader;

    public Text m_debugText;
    public List<EventToColor> m_eventToColor = new  ();

    public Color m_currentColor = Color.white;
    public Color m_wantedColor;

    [System.Serializable]
    public class EventToColor {

        public string m_eventName;
        public Color m_wantedColor;

    }

    public void PushColor(Color color) {
        m_wantedColor = color;
    }

    public void PushEvent(string text) {
        EventToColor []  v = m_eventToColor.Where(k => k.m_eventName.Trim().ToLower()==text.Trim().ToLower()).ToArray() ;
        if (v.Length > 0) {
            foreach (var vc in v)
            {
                m_wantedColor = vc.m_wantedColor;
                m_debugText.text  = vc.m_eventName + ", " + m_debugText.text;
            }
        }
     
    }
    public float m_speed=1;
    private void Update()
    {
        m_currentColor = Color.Lerp(m_currentColor, m_wantedColor, Time.deltaTime* m_speed);
        m_userShader.color = m_currentColor;
        m_userShader.SetColor("_Color", m_currentColor);
    }
}
