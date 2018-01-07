
using Demo.GameLogic.Entities;

namespace Demo.GameLogic.Componnets
{
    interface IPropertyListener
    {
        void OnPropertyValueChanged();
    }

    class Property : Component
    {
        public PropertyChangedDelegate onValueChanged = null;

        // 一级属性
        struct PropertyLevel1
        {
            public float strength;
            public float intelligence;
            public float agility;
        }

        // 二级属性
        struct PropertyLevel2
        {
            public float hp;
            public float mana;
            public float attack;
            public float attackSpeed;
            public float phyArmor;
            public float mageArmor;
        }

        private float m_CurrentHp = 0;
        private float m_CurrentMana = 0;
        private float m_RecoveryHp = 0;
        private float m_RecoveryMana = 0;
        private PropertyLevel1 m_StaticPropertyLevel1;
        private PropertyLevel2 m_StaticPropertyLevel2;
        private PropertyLevel1 m_DynamicPropertyLevel1;
        private PropertyLevel2 m_DynamicPropertyLevel2;

        public Property(Entity entity) : base(entity)
        {
        }

        public float hp
        {
            get { return m_CurrentHp; }
            set
            {
                m_CurrentHp = value;
                if (m_CurrentHp < 0)
                    m_CurrentHp = 0;
                else if (m_CurrentHp > maxHp)
                    m_CurrentHp = maxHp;
            }
        }
        public float maxHp
        {
            get
            {
                return m_StaticPropertyLevel2.hp + m_DynamicPropertyLevel2.hp;
            }
            set
            {
                m_DynamicPropertyLevel2.hp = value - m_StaticPropertyLevel2.hp;
            }
        }
        public float recoveryHp
        {
            get { return m_RecoveryHp; }
            set { m_RecoveryHp = value; }
        }

        public float recoveryMana
        {
            get { return m_RecoveryMana; }
            set { m_RecoveryMana = value; }
        }
        public float mana
        {
            get
            {
                return m_CurrentMana;
            }
            set
            {
                m_CurrentMana = value;
                if (m_CurrentMana < 0)
                    m_CurrentMana = 0;
                else if (m_CurrentMana > maxMana)
                    m_CurrentMana = maxMana;
            }
        }
        public float maxMana
        {
            get
            {
                return m_StaticPropertyLevel2.mana + m_DynamicPropertyLevel2.mana;
            }
            set
            {
                m_DynamicPropertyLevel2.mana = value - m_StaticPropertyLevel2.mana;
            }
        }

        public float attack
        {
            get
            {
                return m_StaticPropertyLevel2.attack + m_DynamicPropertyLevel2.attack;
            }
            set
            {
                m_DynamicPropertyLevel2.attack = value - m_StaticPropertyLevel2.attack;
            }
        }
        public float attackSpeed
        {
            get
            {
                return m_StaticPropertyLevel2.attackSpeed + m_DynamicPropertyLevel2.attackSpeed;
            }
            set
            {
                m_DynamicPropertyLevel2.attackSpeed = value - m_StaticPropertyLevel2.attackSpeed;
            }
        }

        public int magicImmuneBuffCount
        {
            get; set;
        }
        public bool isMagicImmune
        {
            get
            {
                return magicImmuneBuffCount == 0;
            }
        }

        public delegate void PropertyChangedDelegate();
    }
}
