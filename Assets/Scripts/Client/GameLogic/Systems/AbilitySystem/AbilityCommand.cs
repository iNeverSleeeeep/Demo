using Demo.GameLogic.Abilities;
using System.Collections.Generic;

namespace Demo.GameLogic.Systems
{
    struct AbilityCommandContext
    {
        public int caster;
        public int target;

        //public AbilityContext ability;
        public Dictionary<string, IModifierExecutor> modifiers;
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
            m_ModifierName = modifierName;
        }
        public void Execute(AbilityCommandContext ctx)
        {
            var target = Game.Instance.gameLogicManager.entityManager.GetEntity(ctx.target);
            if (target != null)
            {
                IModifierExecutor modifier;
                if (ctx.modifiers.TryGetValue(m_ModifierName, out modifier))
                {
                    modifier = modifier.Clone();
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
            m_DamageType = type;
            m_DamageValue = value;
        }

        public void Execute(AbilityCommandContext ctx)
        {
            var target = Game.Instance.gameLogicManager.entityManager.GetEntity(ctx.target);
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


