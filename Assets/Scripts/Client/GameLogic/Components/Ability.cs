using UnityEngine;
using UnityEditor;
using Demo.GameLogic.Entities;
using Demo.GameLogic.Abilities;
using System.Collections.Generic;
using Demo.GameLogic.Systems;

namespace Demo.GameLogic.Componnets
{
    class Ability : Component
    {
        public string attack;
        public List<string> abilities;
        public List<string> passive;
        public string ultimate;

        public string abilityToCast;
        public int target;

        public AbilityData current;

        public Ability(Entity entity) : base(entity)
        {
            abilities = new List<string>();
            passive = new List<string>();
        }

        public class AbilityData
        {
            public string name = null;

            public float startTime = 0f;

            public AbilityRoot root = null;
        }
    }
}
