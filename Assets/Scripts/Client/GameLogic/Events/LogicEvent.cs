using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Demo.GameLogic.Events
{
    static class LogicEvent
    {
        private static Dictionary<LogicEventType, LogicEventListener> listeners = new Dictionary<LogicEventType, LogicEventListener>();

        public static void Register(LogicEventType type, LogicEventListener toRegister)
        {
            LogicEventListener listener = null;
            if (listeners.TryGetValue(type, out listener) == false)
                listeners.Add(type, toRegister);
            else
                listener += toRegister;
        }

        public static void Unregister(LogicEventType type, LogicEventListener toUnregister)
        {
            LogicEventListener listener = null;
            if (listeners.TryGetValue(type, out listener) == true)
                listener -= toUnregister;
        }

        public static void RaiseEvent(LogicEventType type, object userData)
        {
            LogicEventListener listener = null;
            if (listeners.TryGetValue(type, out listener) && listener != null)
                listener(userData);
        }

        public static void Clear()
        {
            foreach(var listener in listeners)
            {
                if (listener.Value != null)
                    Debug.LogWarning("检查未清理的LogicEvent type=" + listener.Key);
            }
            listeners.Clear();
        }

        public delegate void LogicEventListener(object userData); 
    }
}
