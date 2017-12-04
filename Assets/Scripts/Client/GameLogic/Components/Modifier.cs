using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.GameLogic.Entities;
using Demo.GameLogic.Systems;

namespace Demo.GameLogic.Componnets
{
    class Modifier : Component
    {
        private LinkedList<IModifierExecutor> m_Modifiers = null;

        public Modifier(Entity entity) : base(entity)
        {
            m_Modifiers = new LinkedList<IModifierExecutor>();
        }

        public void AddModifier(IModifierExecutor modifier)
        {
            m_Modifiers.AddLast(modifier);
        }
    }
}
