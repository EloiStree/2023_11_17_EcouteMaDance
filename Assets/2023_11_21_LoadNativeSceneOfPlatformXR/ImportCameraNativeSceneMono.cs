using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImportCameraNativeSceneMono : MonoBehaviour
{

    public string m_cameraSceneIdToUse= "Default";
    public bool m_useAutoDetection = false;

    public AutoDetectionXrToID[] m_useAutoDetectionCode = new AutoDetectionXrToID[]{
     new AutoDetectionXrToID("Oculus"),
     new AutoDetectionXrToID("Lynx"),
     new AutoDetectionXrToID("Pico"),
    };

    public SceneToLoadFromContext[] m_sceneToLoad= {
        new SceneToLoadFromContext("Default", "DefaultCameraXR"),
        new SceneToLoadFromContext("Lynx", "LynxCameraXR"),
        new SceneToLoadFromContext("Oculus", "OculusCameraXR")
    };

    [System.Serializable]
    public class SceneToLoadFromContext
    {
        public string m_cameraSceneId = "Default";
        public string m_cameraSceneName = "CameraXrDefault";

        public SceneToLoadFromContext(string cameraSceneId, string cameraSceneName)
        {
            m_cameraSceneId = cameraSceneId;
            m_cameraSceneName = cameraSceneName;
        }
    }

    void Awake()
    {
        string sceneId = m_cameraSceneIdToUse;
        string sceneName = "";
        if (m_useAutoDetection)
        {
            foreach (var item in m_useAutoDetectionCode)
            {
                if (item != null && item.m_codeOfAutoDetection != null && item.m_codeOfAutoDetection.IsDetectedAsTargetPlatform())
                {
                    sceneId = item.m_xrPlatformIdToLoad;
                }
            }
        }

        for (int i = 0; i < m_sceneToLoad.Length; i++)
        {
            if (m_sceneToLoad[i].m_cameraSceneId.ToLower().Trim() == sceneId.ToLower().Trim()) {
                sceneName = m_sceneToLoad[i].m_cameraSceneName;
                break;
            }
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}

[System.Serializable]
public class AutoDetectionXrToID {

    public string m_xrPlatformIdToLoad;
    public AbstractPlatformId m_codeOfAutoDetection;

    public AutoDetectionXrToID(string xrPlatformIdToLoad)
    {
        m_xrPlatformIdToLoad = xrPlatformIdToLoad;
    }
}

public abstract class AbstractPlatformId : MonoBehaviour {

    public abstract bool IsDetectedAsTargetPlatform();
}