using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static RandomizeMixamoMono;

public class FrameBodyPositionRecorderMono : MonoBehaviour
{



    public UnityEvent m_takeSceenShot;
    public UnityEvent m_disableAnimationScript;
    public UnityEvent m_enableAnimationScript;

    public RandomizeMixamoMono m_source;



    public float m_timeToRecord = 6;
    public float m_timeStart;
    public float m_timecurrent;

    public int m_numberOfScreenshotToTake = 10;
    public int m_screenshotLeft;
    public KeyCode m_recordPose = KeyCode.P;
    public KeyCode m_startRandomScreenshot = KeyCode.M;



    public void TakeScreenshot()
    {
        m_takeSceenShot.Invoke();
        m_screenshotLeft--;
    }
    

    public void TakeScreenshotWithRandom()
    {
        StartCoroutine(ProcessPhoto());
    }
    public float m_delayBetweenAction = 0.1f;
    public IEnumerator ProcessPhoto()
    {

        m_disableAnimationScript.Invoke();
        foreach (var item in m_pose)
        {
            for (int i = 0; i < m_numberOfScreenshotToTake; i++)
            {
                item.Randomize(m_source.m_maxRotation);
                yield return new WaitForSeconds(m_delayBetweenAction);
                m_takeSceenShot.Invoke();
                yield return new WaitForSeconds(m_delayBetweenAction);
                item.Reload();
                yield return new WaitForSeconds(m_delayBetweenAction);
            }
        }
        m_enableAnimationScript.Invoke();
        m_pose.Clear();
    }

    public List<SavePositionFrame> m_pose = new List<SavePositionFrame>();
    public int m_poseCount;
    public void Update()
    {
        if (Input.GetKeyDown(m_recordPose)) {
            m_source.Save();
            SavePositionFrame p = m_source.GetSavePositionFrame("");
           // p.Save();
            m_pose.Add(p);
            m_poseCount = m_pose.Count; }
        if (Input.GetKeyDown(m_startRandomScreenshot)) { TakeScreenshotWithRandom(); }
    }

}
