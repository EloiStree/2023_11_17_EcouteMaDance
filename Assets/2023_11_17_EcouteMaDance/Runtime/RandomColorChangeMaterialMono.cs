using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomColorChangeMaterialMono : MonoBehaviour
{

    public float m_minChangeTiming=1;
    public float m_maxChangeTiming=6;

    public ColorEvent m_colorChangeEvent;
    [System.Serializable]
    public class ColorEvent : UnityEvent<Color> { }

    private void OnEnable()
    {
        StartCoroutine(ChangeColorCoroutine());
    }

    public IEnumerator ChangeColorCoroutine() {

        while (true) {
            m_colorChangeEvent.Invoke(
                new Color(
                UnityEngine.Random.Range(0, 1f),
                UnityEngine.Random.Range(0, 1f),
                UnityEngine.Random.Range(0, 1f),
                UnityEngine.Random.Range(0, 1f))
                );
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(UnityEngine.Random.Range(m_minChangeTiming, m_maxChangeTiming));
            
        }
    }
}
