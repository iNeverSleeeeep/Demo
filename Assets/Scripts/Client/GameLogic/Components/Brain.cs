
using Demo.GameLogic.Entities;

namespace Demo.GameLogic.Componnets
{
    class Brain : Component
    {
        int m_Enemy = 0;
        public int enemy
        {
            get { return m_Enemy; }
            set { m_Enemy = value; }
        }

        public Brain(Entity entity) : base(entity)
        {
        }
    }
}

