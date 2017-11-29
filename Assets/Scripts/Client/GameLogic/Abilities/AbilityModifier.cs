
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic.Abilities
{
    class AbilityModifier : ScriptableObject
    {
        public float duration;
        public float thinkInterval;
        public AbilityModifierTrigger trigger;

        public List<AbilityEvent> events;
    }
}

