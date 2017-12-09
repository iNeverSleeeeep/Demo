using UnityEngine;
using Demo.Frame;

namespace Demo.Input
{
    class StandaloneInputHandler : InputHandler
    {
        bool m_Moving = false;

        public override void Tick()
        {
            var up = UnityEngine.Input.GetKey(KeyCode.UpArrow);
            var down = UnityEngine.Input.GetKey(KeyCode.DownArrow);
            var left = UnityEngine.Input.GetKey(KeyCode.LeftArrow);
            var right = UnityEngine.Input.GetKey(KeyCode.RightArrow);

            float angle = float.NaN;
            if (up && right) angle = 45; // ↗
            else if (up && left) angle = 315; // ↖
            else if (up && down) { }
            else if (right && down) angle = 135; // ↘
            else if (right && left) { }
            else if (down && left) angle = 225; // ↙
            else if (up) angle = 0; // ↑
            else if (right) angle = 90; // →
            else if (down) angle = 180; // ↓
            else if (left) angle = 270; // ←
            else { }
            if (float.IsNaN(angle) == false)
            {
                var data = new FrameData.EntityData()
                {
                    id = Game.Instance.inputManager.entity,
                    type = FrameData.OperationType.Moveing,
                    angle = angle
                };
                m_FrameData.Add(data);
                m_Moving = true;
            }
            else if (m_Moving)
            {
                var data = new FrameData.EntityData()
                {
                    id = Game.Instance.inputManager.entity,
                    type = FrameData.OperationType.EndMove
                };
                m_FrameData.Add(data);
            }
        }
    }
}

