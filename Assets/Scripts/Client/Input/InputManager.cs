using Demo.Frame;
using Demo.Net;
using Demo.Utils;

namespace Demo.Input
{
    sealed class InputManager
    {
        int m_Entity = 0;
        public int entity { get { return m_Entity; } set { m_Entity = value; } }

        InputHandler m_InputHandler = null;

        public InputManager()
        {
            m_InputHandler = new StandaloneInputHandler();
            Game.Instance.frameDataCollector.AddProvider(m_InputHandler);
        }

        public void Tick()
        {
            m_InputHandler.Tick();
        }
    }
}


