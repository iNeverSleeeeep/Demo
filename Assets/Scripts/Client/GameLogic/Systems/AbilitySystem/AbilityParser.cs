using Demo.GameLogic.Abilities;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic.Systems
{
    class AbilityParser
    {
        public AbilityRoot Parse(DataDrivenAbility data)
        {
            AbilityRoot root = new AbilityRoot(data.keepTime);

            foreach (var abilityEvent in ParseAbilityEvent(data))
                root.AddAbilityEvent(abilityEvent);

            if (data.modifiers != null)
                foreach (var modifier in data.modifiers)
                    root.modifiers.Add(modifier.name, ParseAbilityModifier(modifier));
            
            return root;
        }

        public AbilityCondition ParseCondition(DataDrivenAbility ability)
        {
            var condition = new AbilityCondition()
            {
                castRange = ability.castRange,
                manaCost = ability.manaCost
            };
          
            return condition;
        }

        private List<IAbilityExecutor> ParseAbilityEvent(DataDrivenAbility ability)
        {
            List<IAbilityExecutor> executors = new List<IAbilityExecutor>();

            if (ability.OnTime != null)
                foreach (var command in ability.OnTime)
                    executors.Add(new AbilityTimer(ParseCommands(command), command.startTime));

            if (ability.OnSpellStart != null)
                foreach (var command in ability.OnSpellStart)
                    executors.Add(new AbilityTirgger(ParseCommands(command), AbilityEventTrigger.OnSpellStart));

            return executors;
        }

        private ModifierRoot ParseAbilityModifier(AbilityModifier abilityModifier)
        {
            ModifierRoot root = new ModifierRoot(abilityModifier.duration);

            if (abilityModifier.OnIntervalThink != null)
                foreach (var command in abilityModifier.OnIntervalThink)
                    root.AddModifier(new ModifierThinkInterval(ParseCommands(command), command.thinkInterval));

            return root;
        }

        private List<IAbilityCommand> ParseCommands(EventCommand command)
        {
            List<IAbilityCommand> cmds = new List<IAbilityCommand>();
            if (string.IsNullOrEmpty(command.applyModifier.modifierName) == false)
                cmds.Add(new ApplyModifierCommand(command.applyModifier.modifierName));
            if ((int)command.damage.type != 0 && command.damage.value != 0)
                cmds.Add(new DamageCommand(command.damage.type, command.damage.value));

            return cmds;
        }
    }
}
