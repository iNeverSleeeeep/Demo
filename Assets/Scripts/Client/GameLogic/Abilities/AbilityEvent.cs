
using UnityEngine;

namespace Demo.GameLogic.Abilities
{
    class AbilityEvent : ScriptableObject
    {
        public AbilityEventTrigger trigger;
        public ApplyModifier applyModifier;
        public TrackingProjectile trackingProjectile;
        public Damage damage;
        public ApplyMotionController applyMotionController;
        public RunScript runScript;
    }
}

