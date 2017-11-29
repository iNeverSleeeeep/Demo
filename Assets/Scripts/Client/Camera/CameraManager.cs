using Cinemachine;
using UnityEngine;

namespace Demo.Camera
{
    class CameraManager
    {
        CinemachineVirtualCamera m_CMCamera = null;

        UnityEngine.Camera m_Camera = null;
        public UnityEngine.Camera camera
        {
            get
            {
                if (m_Camera == null)
                    m_Camera = UnityEngine.Camera.main;
                return m_Camera;
            }
        }

        Transform m_Target = null;

        public Transform target
        {
            get { return m_Target; }
            set
            {
                m_Target = value;
                m_CMCamera.LookAt = value;
                m_CMCamera.Follow = value;

                var transposer = m_CMCamera.GetCinemachineComponent<CinemachineTransposer>();
                transposer.m_FollowOffset = new Vector3(0, 10, -6);
                transposer.m_XDamping = transposer.m_YawDamping = transposer.m_ZDamping = 0;
            }
        }

        public CameraManager()
        {
            m_CMCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        }
    }
}

