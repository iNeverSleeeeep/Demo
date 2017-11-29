
using Demo.GameLogic.Entities;

namespace Demo.GameLogic.Componnets
{
    class Movement : Component
    {
        float m_DefaultSpeed = 2f;
        public float defaultSpeed
        {
            get { return m_DefaultSpeed; }
            set { m_DefaultSpeed = value; }
        }
        float m_Speed = 0f;
        public float speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }

        float m_Angle = float.NaN;
        public float angle
        {
            get { return m_Angle; }
            set { m_Angle = value; }
        }

        public Movement(Entity entity) : base(entity)
        {

        }
    }
}

