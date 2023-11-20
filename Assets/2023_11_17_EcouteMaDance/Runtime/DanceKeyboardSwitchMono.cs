using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DanceKeyboardSwitchMono : MonoBehaviour
{
    public KeyCode m_previous = KeyCode.LeftArrow;
    public KeyCode m_next = KeyCode.RightArrow;
    public UnityEvent m_previousEvent;
    public UnityEvent m_nextEvent;

    void Update()
    {
        if (Input.GetKeyDown(m_previous)) m_previousEvent.Invoke();
        if (Input.GetKeyDown(m_next)) m_nextEvent.Invoke();
    }
}
