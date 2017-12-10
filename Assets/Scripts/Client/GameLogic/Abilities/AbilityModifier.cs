using System;

namespace Demo.GameLogic.Abilities
{
    [Serializable]
    class AbilityModifier
    {
        public string name;
        public float duration;

        public EventCommand[] OnIntervalThink;
    }
}

