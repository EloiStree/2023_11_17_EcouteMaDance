using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSyncAnimatorMono : MonoBehaviour
{
    public Animator m_sourceAnimator;
    public Animator m_targetAnimator;
    public string m_animName = "Dance22h";
    public float m_sourceNormalizedTime;

    void Update()
    {
        // Check if the source and target Animators are assigned
        if (m_sourceAnimator != null && m_targetAnimator != null)
        {
            // Get the normalized time from the source Animator
            m_sourceNormalizedTime = m_sourceAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            
            // Set the normalized time for the target Animator
            SetTargetAnimatorTime(m_sourceNormalizedTime);
        }
       
    }

    void SetTargetAnimatorTime(float normalizedTime)
    {
        // Check if the target Animator component is assigned
        if (m_targetAnimator != null)
        {
            // Calculate the time in seconds based on the normalized time
            float totalTime = m_targetAnimator.GetCurrentAnimatorStateInfo(0).length;
            float timeInSeconds = totalTime * normalizedTime;

            // Play the animation from the beginning and set the time
            m_targetAnimator.Play(m_animName, 0, normalizedTime);
        }
        
    }
}

