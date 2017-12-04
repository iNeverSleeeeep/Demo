using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    class CoroutineManager
    {

        public CoroutineManager()
        {
            m_Enumrators = new List<IEnumerator>();
        }

        #region Logic Coroutine
        List<IEnumerator> m_Enumrators = null;
        bool m_InTicking = false;

        public void StartLogic(IEnumerator enumerator)
        {
            Debug.Assert(m_InTicking == false);
            m_Enumrators.Add(enumerator);
        }

        public void StopLogic(IEnumerator enumerator)
        {
            Debug.Assert(m_InTicking == false);
            if (m_Enumrators.Contains(enumerator))
                m_Enumrators.Remove(enumerator);
        }

        public void Tick()
        {
            m_InTicking = true;
            for (var i = m_Enumrators.Count - 1; i >= 0; --i)
            {
                if (m_Enumrators[i].MoveNext() == false)
                    m_Enumrators.RemoveAt(i);
            }
            m_InTicking = false;
        }
        #endregion

        #region Unity Coroutine
        class CoroutineHandler : MonoBehaviour { }
        CoroutineHandler m_CoroutineHandler = null;
        CoroutineHandler coroutineHandler
        {
            get
            {
                if (m_CoroutineHandler == null)
                {
                    var go = new GameObject();
                    go.hideFlags = HideFlags.HideAndDontSave;
                    m_CoroutineHandler = go.AddComponent<CoroutineHandler>();
                }
                return m_CoroutineHandler;
            }
        }

        public Coroutine Start(IEnumerator enumerator)
        {
            return coroutineHandler.StartCoroutine(enumerator);
        }

        public void Stop(IEnumerator enumerator)
        {
            coroutineHandler.StopCoroutine(enumerator);
        }

        #endregion
    }

    class WaitForLogicSeconds : CustomYieldInstruction
    {
        private float m_Time;
        private float m_Current;

        public WaitForLogicSeconds(float time)
        {
            m_Current = 0;
            m_Time = time;
        }

        public override bool keepWaiting
        {
            get
            {
                m_Current += Utils.Time.logicDeltaTime;
                return m_Current > m_Time;
            }
        }
    }
}


