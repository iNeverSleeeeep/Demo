using Demo.GameLogic.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic.Systems
{
    struct AbilityCommandContext
    {
        public Entity caster;
        public Entity target;
    }

    interface IAbilityCommand
    {
        void Execute(AbilityCommandContext ctx);
        void Reverse(AbilityCommandContext ctx);
    }
}


