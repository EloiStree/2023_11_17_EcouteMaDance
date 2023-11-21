
using System.Collections;
using UnityEngine;

namespace Lynx
{
    public class SetARModeMono : MonoBehaviour
    {
        [Tooltip("Time between each switch (AR/VR).")]
        [SerializeField] private float m_timer = 5.0f;

        public bool IsRunning { get; set; } = false;

        IEnumerator Start()
        {
            IsRunning = true;

            while (IsRunning)
            {
                yield return new WaitForSecondsRealtime(m_timer);
                LynxAPI.ToggleAR();
                LynxAPI.ToggleAROnly();
                LynxAPI.SetVR(false);
                LynxAPI.SetAR(true);
                LynxAPI.SetAROnly(true);

            }
        }

        private void OnApplicationQuit()
        {
            IsRunning = false;
        }


    }
}
