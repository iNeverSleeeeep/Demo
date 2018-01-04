using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    class CoroutineManager
    {
        public CoroutineManager()
        {
            
            m_Enumerators = new List<IEnumerator>();
            m_TempEnumerators = new List<IEnumerator>();
            m_DeleteEnumerators = new List<IEnumerator>();
        }

        #region Logic Coroutine
        List<IEnumerator> m_Enumerators = null;
        List<IEnumerator> m_TempEnumerators = null;
        List<IEnumerator> m_DeleteEnumerators = null;

        public void StartLogic(IEnumerator enumerator)
        {
            m_Enumerators.Add(enumerator);
        }

        public void StopLogic(IEnumerator enumerator)
        {
            if (m_Enumerators.Contains(enumerator))
                m_Enumerators.Remove(enumerator);
        }

        public void Tick()
        {
            m_TempEnumerators.Clear();
            m_TempEnumerators.AddRange(m_Enumerators);
            m_DeleteEnumerators.Clear();
            for (var i = m_TempEnumerators.Count - 1; i >= 0; --i)
            {
                var enumerator = m_TempEnumerators[i];
                var cyi = enumerator.Current as CustomYieldInstruction;
                if (cyi != null && cyi.keepWaiting)
                    continue;
                var next = enumerator.MoveNext();
                if (next == false)
                    m_DeleteEnumerators.Add(enumerator);
            }
            foreach(var enumerator in m_DeleteEnumerators)
                if (m_Enumerators.Contains(enumerator))
                    m_Enumerators.Remove(enumerator);
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
                return m_Current < m_Time;
            }
        }
    }
}


