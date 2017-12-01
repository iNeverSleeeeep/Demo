
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic.Abilities
{
    class AbilityModifier
    {
        public string name;
        public float duration;
        public float thinkInterval;
        public AbilityModifierTrigger trigger;

        public List<AbilityEvent> events;
    }
}

