using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbFollowTarget : MonoBehaviour
{
    
    public Transform [] m_followTarget;

    
    void LateUpdate()
    {
         MoveToPosition();
    }

    private void MoveToPosition()
    {
        foreach (var item in m_followTarget)
        {
            if (item!=null && item.gameObject.activeInHierarchy)
            {
                transform.position =item.position;
                transform.rotation = item.rotation;
                    return;
            }

        }
    }
}
