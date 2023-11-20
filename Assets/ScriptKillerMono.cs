using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptKillerMono : MonoBehaviour
{
    public Behaviour[] m_targetToKill;

[ContextMenu("Kill targets")]
    public void KillTargets()
    {
        for (int i = 0; i < m_targetToKill.Length; i++)
        {
            if (m_targetToKill != null) { 
                

                DestroyImmediate(m_targetToKill[i]);
            
            }
        }
    }
}
