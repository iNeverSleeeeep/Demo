
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic
{
    sealed class LogicTickManager
    {
        List<ITickable> m_Tickables = null;
        bool m_Ticking = false;

        public LogicTickManager()
        {
            m_Tickables = new List<ITickable>();
        }

        public void AddTickable(ITickable tickable)
        {
            Debug.Assert(m_Ticking == false);
            Debug.Assert(m_Tickables.Contains(tickable) == false);
            m_Tickables.Add(tickable);
        }

        public void RemoveTickable(ITickable tickable)
        {
            Debug.Assert(m_Ticking == false);
            Debug.Assert(m_Tickables.Contains(tickable));
            m_Tickables.Remove(tickable);
        }

        public void Tick()
        {
            m_Ticking = true;
            foreach (var tickable in m_Tickables)
                tickable.Tick();
            m_Ticking = false;
        }
    }
}
