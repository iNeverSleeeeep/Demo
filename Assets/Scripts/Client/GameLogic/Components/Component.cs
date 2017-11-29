
using Demo.GameLogic.Entities;

namespace Demo.GameLogic.Componnets
{
    abstract class Component
    {
        readonly Entity m_Entity = null;
        public Entity entity { get { return m_Entity; } }

        bool m_Enabled = false;
        public bool enabled
        {
            get { return m_Enabled; }
            set
            {
                if (m_Enabled == value) return;
                m_Enabled = value;
                if (m_Enabled)
                    OnEnable();
                else
                    OnDisable();
            }
        }

        public Component(Entity entity)
        {
            m_Entity = entity;
        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }

        public virtual void Destroy()
        {
            enabled = false;
        }
    }
}


