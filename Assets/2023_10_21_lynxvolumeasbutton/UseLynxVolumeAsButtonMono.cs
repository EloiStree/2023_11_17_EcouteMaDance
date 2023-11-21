using Lynx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UseLynxVolumeAsButtonMono : MonoBehaviour
{

    [SerializeField] private AudioMng audioMng;


    public UnityEvent m_onVolumeChanged;
    public UnityEvent m_onVolumeUp;
    public UnityEvent m_onVolumeDown;
    public VolumeEvent m_newVolumePercent;

    public float m_previousVolume;
    public float m_currentVolume;

    [System.Serializable]
    public class VolumeEvent : UnityEvent<float>{}

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidComMng.Instance().mAudioVolumeChangeEvent.AddListener(OnAudioVolumeChanged);       
#endif
    }

   
    private void OnAudioVolumeChanged(int volume)
    {
        m_currentVolume = m_previousVolume;
        bool volumechange = volume != m_previousVolume;
        bool volumechangeUp = volume > m_previousVolume;
        if (volumechange)
        {
            m_onVolumeChanged.Invoke();
            if (volumechangeUp)
                m_onVolumeUp.Invoke();
            else
                m_onVolumeDown.Invoke();
            m_newVolumePercent.Invoke(volume);
        }
    }
}
