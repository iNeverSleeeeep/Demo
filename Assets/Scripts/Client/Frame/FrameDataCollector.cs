
using Demo.Net;
using System.Collections.Generic;

namespace Demo.Frame
{
    class FrameDataCollector
    {
        List<IFrameDataProvider> m_Providers = null;

        public FrameDataCollector()
        {
            m_Providers = new List<IFrameDataProvider>();
        }

        public void AddProvider(IFrameDataProvider provider)
        {
            m_Providers.Add(provider);
        }

        public void RemoveProvider(IFrameDataProvider provider)
        {
            m_Providers.Remove(provider);
        }

        public void Tick()
        {
            FrameData frameData = default(FrameData);
            frameData.id = Utils.Time.logicFrameCount;

            FrameData tempFrameData = default(FrameData);
            foreach (var provider in m_Providers)
            {
                if (provider.GetFrameData(ref tempFrameData))
                {
                    if (frameData.data == null)
                        frameData.data = new List<FrameData.EntityData>();
                    frameData.data.AddRange(tempFrameData.data);
                }
            }
            if (frameData.data != null && frameData.data.Count > 0)
                Connection.Send(frameData);
        }
    }
}