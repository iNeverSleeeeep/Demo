using Demo.GameLogic.Abilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic.Systems
{
    class AbilityParser
    {
        public IAbilityExecutor Parse(DataDrivenAbility data)
        {
            AbilityRoot root = new AbilityRoot();
            IAbilityExecutor current = root;
            if (data.events != null)
            {
                foreach (var abilityEvent in data.events)
                {
                    var executor = ParseAbilityEvent(abilityEvent);
                    current.next = executor;
                    current = executor;
                }
            }

            if (data.modifiers != null)
            {
                foreach (var modifier in data.modifiers)
                {
                    var executor = ParseAbilityModifier(modifier);
                    root.modifiers.Add(modifier.name, executor);
                }
            }
            
            return root;
        }

        private IAbilityExecutor ParseAbilityEvent(AbilityEvent abilityEvent)
        {
            IAbilityExecutor executor = null;

            return executor;
        }

        private IModifierExecutor ParseAbilityModifier(AbilityModifier abilityModifier)
        {
            IModifierExecutor executor = null;

            return executor;
        }
    }
}
