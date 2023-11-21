using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    using UnityEngine.XR;

public class DefaultPlatformXrAutoDetectIdMono : AbstractPlatformId
{
    public string m_lookingForProvider = "InsertProviderName";
    public string m_currentProviderName = "";

    void Start()
    {
        CheckOpenXRProvider();
    }

    [ContextMenu("Check")]
   public  void CheckOpenXRProvider()
    {
        string activeProvider = XRSettings.loadedDeviceName;

        if (string.IsNullOrEmpty(activeProvider))
        {
            Debug.Log("No XR provider is currently active.");
            m_currentProviderName = "None";
        }
        else
        {
            Debug.Log("Active XR provider: " + activeProvider);
            m_currentProviderName = activeProvider;
        }
    }

    public override bool IsDetectedAsTargetPlatform()
    {
        return m_lookingForProvider.ToLower().Trim() == m_currentProviderName.ToLower().Trim();
    }

}
