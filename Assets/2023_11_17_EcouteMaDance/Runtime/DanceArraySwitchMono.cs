using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceArraySwitchMono : MonoBehaviour
{

    public GameObject[] m_array;
    public int m_index;

    public bool m_refreshAtAwake;
    private void Awake()
    {
        if (m_refreshAtAwake) Refresh();
    }
    [ContextMenu("Refresh")]
    public void Refresh()
    {

        for (int i = 0; i < m_array.Length; i++)
        {
            m_array[i].SetActive(i == m_index);
        }
    }

    [ContextMenu("Next")]
    public void Next()
    {
        m_index++;
        if (m_index >= m_array.Length)
            m_index = 0;
        Refresh();
    }

    [ContextMenu("Previous")]
    public void Previous()
    {

        m_index--;
        if (m_index < 0)
            m_index = m_array.Length - 1;


        Refresh();
    }

}
