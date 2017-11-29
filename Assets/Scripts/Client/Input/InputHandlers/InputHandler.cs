
using Demo.Frame;
using System.Collections.Generic;

namespace Demo.Input
{
    abstract class InputHandler : IFrameDataProvider
    {
        protected List<FrameData.EntityData> m_FrameData = null;

        public InputHandler()
        {
            m_FrameData = new List<FrameData.EntityData>();
        }

        public abstract void Tick();

        public bool GetFrameData(ref FrameData data)
        {
            if (m_FrameData.Count > 0)
            {
                data.id = Utils.Time.logicFrameCount;
                data.data = m_FrameData;
                m_FrameData = new List<FrameData.EntityData>();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

