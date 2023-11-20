using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FramerScreenshot : MonoBehaviour
{

    public UnityEvent m_takeSceenShot;
    public UnityEvent m_requestToRandomized;
    public UnityEvent m_resetBodyPosition;
    public UnityEvent m_disableAnimationScript;
    public UnityEvent m_enableAnimationScript;

    public float m_timeToRecord = 6;
    public float m_timeStart;
    public float m_timecurrent;

    public int m_numberOfScreenshotToTake=10;
    public int m_screenshotLeft;
    public KeyCode m_takeScreenshot = KeyCode.F1;
    public KeyCode m_startRandomScreenshot = KeyCode.F2;



    public void TakeScreenshot()
    {
        m_takeSceenShot.Invoke();
        m_screenshotLeft--;
    }
    public void AskRandom()
    {
        m_requestToRandomized.Invoke();
    }

    public void TakeScreenshotWithRandom()
    {
        StartCoroutine(ProcessPhoto());
    }

    public IEnumerator ProcessPhoto() {

        m_disableAnimationScript.Invoke();
        for (int i = 0; i < m_numberOfScreenshotToTake; i++)
        {
            AskRandom();
            yield return new WaitForSeconds(0.3f);
            m_takeSceenShot.Invoke();
            yield return new WaitForSeconds(0.3f);
            m_resetBodyPosition.Invoke();
            yield return new WaitForSeconds(0.3f);
        }
        m_enableAnimationScript.Invoke();
    }

   
    public void Update()
    {
        if (Input.GetKeyDown(m_takeScreenshot)) { TakeScreenshot(); }
        if (Input.GetKeyDown(m_startRandomScreenshot)) { TakeScreenshotWithRandom(); }
    }

}
