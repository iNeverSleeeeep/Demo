using UnityEngine;
using Demo.Frame;

namespace Demo.Input
{
    class StandaloneInputHandler : InputHandler
    {
        bool m_Moving = false;

        public override void Tick()
        {
            var w = UnityEngine.Input.GetKey(KeyCode.W);
            var s = UnityEngine.Input.GetKey(KeyCode.S);
            var a = UnityEngine.Input.GetKey(KeyCode.A);
            var d = UnityEngine.Input.GetKey(KeyCode.D);

            float angle = float.NaN;
            if (w && d) angle = 45; // ↗
            else if (w && a) angle = 315; // ↖
            else if (w && s) { }
            else if (d && s) angle = 135; // ↘
            else if (d && a) { }
            else if (s && a) angle = 225; // ↙
            else if (w) angle = 0; // ↑
            else if (d) angle = 90; // →
            else if (s) angle = 180; // ↓
            else if (a) angle = 270; // ←
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

            //Debug.Log("UnityEngine.Input.GetKeyDown(KeyCode.Z)="+ UnityEngine.Input.GetKeyDown(KeyCode.Z));
            //Debug.Log("UnityEngine.Input.GetKey(KeyCode.Z)=" + UnityEngine.Input.GetKey(KeyCode.Z));
            if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
            {
                var data = new FrameData.EntityData()
                {
                    id = Game.Instance.inputManager.entity,
                    type = FrameData.OperationType.Ability,
                    abilityName = "TestAbility"
                };
                m_FrameData.Add(data);
                Debug.Log("?????????????????????");
            }
        }
    }
}

