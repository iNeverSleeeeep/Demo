using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.GameLogic.Entities;
using Demo.GameLogic.Systems;
using UnityEngine;

namespace Demo.GameLogic.Componnets
{
    class Modifier : Component
    {
        private LinkedList<ModifierRoot> m_Modifiers = null;

        public Modifier(Entity entity) : base(entity)
        {
            m_Modifiers = new LinkedList<ModifierRoot>();
        }

        public void AddModifier(ModifierRoot modifier)
        {
            m_Modifiers.AddLast(modifier);
            modifier.Execute();
        }

        public void RemoveModifier(ModifierRoot modifier)
        {
            if (m_Modifiers.Contains(modifier))
                m_Modifiers.Remove(modifier);
        }

        protected override void OnDisable()
        {
            while(m_Modifiers.Count > 0)
                m_Modifiers.First.Value.Kill();
            base.OnDisable();
        }
    }
}
