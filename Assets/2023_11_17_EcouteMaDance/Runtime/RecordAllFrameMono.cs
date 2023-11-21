using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecordAllFrameMono : MonoBehaviour
{
    public UnityEvent m_takeSceenShot;
    public KeyCode m_startRecord = KeyCode.KeypadDivide;
    public KeyCode m_stopRecord = KeyCode.KeypadMinus;
    public KeyCode m_invokeRepeat = KeyCode.KeypadPeriod;
    public bool m_record;
    public float m_invokeTime=0.1f;

    public void Update()
    {
        if (Input.GetKeyDown(m_startRecord)) { m_record = true; }
        if (Input.GetKeyDown(m_invokeRepeat)) { 
            
            InvokeRepeating("Push", 0, m_invokeTime);
        }
        if (Input.GetKey(m_stopRecord)) { m_record = false; }

        if (m_record)
            m_takeSceenShot.Invoke();
    }
    public void Push() {
        m_takeSceenShot.Invoke();
    }


}
