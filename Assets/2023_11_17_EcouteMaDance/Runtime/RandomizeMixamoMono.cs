using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomizeMixamoMono : MonoBehaviour
{

    public Transform[] m_pointToAffect;
    public float m_maxRotation=0.1f;


    public SavePositionFrame GetSavePositionFrame(string name) {

        SavePositionFrame n = new SavePositionFrame();
        foreach (var item in m_resetPosition)
        {
            n.m_resetPosition.Add(new SavePosition(item.m_target));
        }
        n.Save();

        return n;
    }
   
    public class SavePositionFrame {
        public string m_name;
        public List<SavePosition> m_resetPosition = new List<SavePosition>();
        public SavePositionFrame()
        {
            m_name = "";
            m_resetPosition = new List<SavePosition>();
        }
        public SavePositionFrame(string name, List<SavePosition> resetPosition)
        {
            m_name = name;
            m_resetPosition = resetPosition;
        }

        [ContextMenu("Save")]
        public void Save()
        {
            foreach (var item in m_resetPosition)
            {
                item.RecordPosition();
            }
        }
        [ContextMenu("Reload")]
        public void Reload()
        {
            foreach (var item in m_resetPosition)
            {
                item.ResetPosition();
            }
        }

       
        public void Randomize(float maxAngle=5)
        {
            Reload();
            foreach (var item in m_resetPosition.Select(k=>k.m_target))
            {
                item.Rotate(new Vector3(GetR(maxAngle), GetR(maxAngle), GetR(maxAngle)), Space.Self);
            }
        }
        float GetR(float maxAngle = 5) { return Random.Range(-maxAngle, maxAngle); }
    }

     List<SavePosition> m_resetPosition = new List<SavePosition>();
    public class SavePosition {
        public Transform m_target;
        public Vector3 m_localPosition;
        public Quaternion m_localRotation;

        public SavePosition(Transform target)
        {
            m_target = target;
            m_localPosition = new();
            m_localRotation = new();
        }

        [ContextMenu("Record Position")]
        public void RecordPosition()
        {
            m_localPosition = m_target.localPosition;
            m_localRotation = m_target.localRotation;
        }
        public void ResetPosition()
        {

             m_target.localPosition =m_localPosition;
              m_target.localRotation= m_localRotation;
        }
    }
    private void Awake()
    {
        foreach (var item in m_pointToAffect)
        {
            m_resetPosition.Add(new SavePosition(item));
        }
        Save();
    }

    [ContextMenu("Save")]
    public void Save()
    {
        foreach (var item in m_resetPosition)
        {
            item.RecordPosition();
        }
    }
    [ContextMenu("Reload")]
    public void Reload()
    {
        foreach (var item in m_resetPosition)
        {
            item.ResetPosition();
        }
    }

    [ContextMenu("Randomize")]
    public void Randomize() {
        foreach (var item in m_pointToAffect)
        {
            item.Rotate(new Vector3(GetR(), GetR(), GetR()), Space.Self);
        }
    }
    float GetR() { return Random.Range(-m_maxRotation, m_maxRotation); }
}
