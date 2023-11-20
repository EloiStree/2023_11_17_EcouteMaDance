using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static CSVToPoseProbabilityMono;

public class Temp_AnimationToEventTriggeringMono : MonoBehaviour
{

    public Animator m_animator;
    public CSVToPoseProbabilityMono m_source;

    void Start()
    {

    }

    public float m_percentOfAnimation;
    public float m_msOfAnimation;
    public float m_msOfAnimationPrevious;

    [System.Serializable]
    public class UnityStringEvent: UnityEvent<string>{}
    public UnityStringEvent m_positionEvent;

    void Update()
    {
        float t = 0;
        if (m_animator)
        {
            try {
                float currentTime = m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                float clipDuration = m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
                float currentTimeInSeconds = (currentTime * clipDuration) * 1000f;
                m_msOfAnimationPrevious = m_msOfAnimation;
                m_percentOfAnimation = currentTime;
                m_msOfAnimation = currentTimeInSeconds;

                var events = m_source.eventFound.Where(k => k.m_milliseconds > m_msOfAnimationPrevious && k.m_milliseconds <= m_msOfAnimation);
                foreach (EventFound e in events)
                {
                    m_positionEvent.Invoke(e.m_poseName);
                }
            }
            catch (Exception e) { //DIRTY CODE IGNORE.
                                  //
                                  }
        }
       
    }
}
