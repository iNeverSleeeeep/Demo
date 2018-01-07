using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Demo.UI
{
    class UIManager
    {
        private LinkedList<BasePanel> m_OpenedPanels;

        public UIManager()
        {
            m_OpenedPanels = new LinkedList<BasePanel>();
        }

        public void Open<T>() where T : BasePanel, new()
        {
            var panel = new T();
            Game.Instance.coroutineManager.Start(panel.Open());
            m_OpenedPanels.AddFirst(panel);
        }

        public void Close<T>() where T : BasePanel
        {
            foreach (var panel in m_OpenedPanels)
            {
                if (panel is T)
                {
                    Game.Instance.coroutineManager.Start(panel.Close());
                    m_OpenedPanels.Remove(panel);
                    return;
                }
            }
        }

        public void CloseTop()
        {
            Game.Instance.coroutineManager.Start(m_OpenedPanels.First.Value.Close());
            m_OpenedPanels.RemoveFirst();
        }
    }
}

