using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demo.Frame;
using Demo.Net;
using Demo;

public class DummyServer : MonoBehaviour
{
    Queue<FrameData> m_Data = null;
    int m_FrameCount = 0;

    private void Start()
    {
        m_Data = new Queue<FrameData>();
        StartCoroutine(FrameLoop());
    }

    public void OnMessage(FrameData data)
    {
        m_Data.Enqueue(data);
    }

    public IEnumerator FrameLoop()
    {
        var time = 0f;
        while (true)
        {
            while(Game.Instance.gameStart)
            {
                time += Time.deltaTime;
                if (time > Demo.Utils.Time.kLogicDeltaTime)
                {
                    time -= Demo.Utils.Time.kLogicDeltaTime;
                    BroadCastFrame();
                }
                yield return null;
            }
            time = 0;
            yield return null;
        }
    }

    public void BroadCastFrame()
    {
        var data = new FrameData()
        {
            id = m_FrameCount++,
            data = new List<FrameData.EntityData>()
        };
        while (m_Data.Count > 0)
            data.data.AddRange(m_Data.Dequeue().data);
        Connection.Receive(data);
    }
}
