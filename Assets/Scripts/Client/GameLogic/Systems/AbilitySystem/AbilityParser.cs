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
            IAbilityExecutor executor = new AbilityRoot();

            return executor;
        }

        private IAbilityExecutor ParseAbilityEvent(AbilityEvent abilityEvent)
        {
            IAbilityExecutor executor = null;

            return executor;
        }

        private IAbilityExecutor ParseAbilityModifier(AbilityModifier abilityModifier)
        {
            IAbilityExecutor executor = null;

            return executor;
        }
    }
}
