using System.Collections;
using System.Collections.Generic;

namespace Demo.Frame
{
    class FrameBuffer
    {
        Queue<FrameData> m_Frames = null;

        public bool empty { get { return m_Frames.Count == 0; } }

        public FrameBuffer()
        {
            m_Frames = new Queue<FrameData>();
        }

        public FrameData GetOneFrame()
        {
            return m_Frames.Dequeue();
        }

        public void AddOneFrame(FrameData data)
        {
            m_Frames.Enqueue(data);
        }
    }
}

