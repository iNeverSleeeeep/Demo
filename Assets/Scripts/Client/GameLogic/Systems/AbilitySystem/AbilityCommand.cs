using Demo.GameLogic.Abilities;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic.Systems
{
    struct AbilityCommandContext
    {
        public int caster;
        public int target;

        //public AbilityContext ability;
        public Dictionary<string, ModifierRoot> modifiers;
    }

    interface IAbilityCommand
    {
        void Execute(AbilityCommandContext ctx);
        void Reverse(AbilityCommandContext ctx);
    }

    class ApplyModifierCommand : IAbilityCommand
    {
        private string m_ModifierName;
        public ApplyModifierCommand(string modifierName)
        {
            Debug.Log("ApplyModifier modifierName="+ modifierName);
            m_ModifierName = modifierName;
        }
        public void Execute(AbilityCommandContext ctx)
        {
            var entityManager = Game.Instance.gameLogicManager.entityManager;
            var target = entityManager.GetEntity(ctx.target);
            if (target != null)
            {
                ModifierRoot modifier;
                if (ctx.modifiers.TryGetValue(m_ModifierName, out modifier))
                {
                    modifier = modifier.Clone() as ModifierRoot;
                    modifier.context = new ModifierContext()
                    {
                        caster = ctx.caster,
                        owner = target.id,
                    };

                    target.modifier.AddModifier(modifier);
                }
            }
        }
        public void Reverse(AbilityCommandContext ctx)
        {

        }
    }

    class DamageCommand : IAbilityCommand
    {
        private AbilityUnitDamageType m_DamageType;
        private float m_DamageValue;

        public DamageCommand(AbilityUnitDamageType type, float value)
        {
            Debug.Log("DamageCommand value=" + value);
            m_DamageType = type;
            m_DamageValue = value;
        }

        public void Execute(AbilityCommandContext ctx)
        {
            var entityManager = Game.Instance.gameLogicManager.entityManager;
            var target = entityManager.GetEntity(ctx.target);
            if (target != null)
            {
                AbilityHelper.DoDamage(m_DamageType, m_DamageValue, target);
            }
        }

        public void Reverse(AbilityCommandContext ctx)
        {
            
        }
    }
}


