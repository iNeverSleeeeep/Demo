using UnityEngine;
using UnityEditor;
using Demo.GameLogic.Entities;
using Demo.GameLogic.Abilities;
using System.Collections.Generic;

namespace Demo.GameLogic.Componnets
{
    class Ability : Component
    {
        public string attack = null;
        public List<string> abilities = null;
        public List<string> passive = null;
        public string ultimate = null;

        public Queue<string> abilitiesToCast = null;

        public AbilityData current = null;

        public Ability(Entity entity) : base(entity)
        {
            abilities = new List<string>();
            passive = new List<string>();
            abilitiesToCast = new Queue<string>();
        }

        public class AbilityData
        {
            public string name = null;

            public float startTime = 0f;
        }
    }
}
