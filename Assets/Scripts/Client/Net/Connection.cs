using Demo.Frame;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Net
{
    static class Connection
    {
        static DummyServer server = null;
        public static void Send(FrameData data)
        {
            //Debug.Log("Connection Send:"+data.ToString());
            if (server == null)
                server = GameObject.FindObjectOfType<DummyServer>();
            if (server != null)
                server.OnMessage(data);
        }

        public static void Receive(FrameData data)
        {
            //Debug.Log("Connection Receive:" + data.ToString());
            Game.Instance.frameBuffer.AddOneFrame(data);
        }
    }
}

