
using System;
using UnityEngine;

namespace Demo.GameLogic.Abilities
{
    [Serializable]
    class EventCommand
    {
        public float startTime;
        public float thinkInterval;

        public ApplyModifier applyModifier;
        public TrackingProjectile trackingProjectile;
        public LinearProjectile linearProjectile;
        public Damage damage;
        public ApplyMotionController applyMotionController;
        public RunScript runScript;
        public AttackSpeed attackSpeed;
        public MagicImmune magicImmune;
    }
}

