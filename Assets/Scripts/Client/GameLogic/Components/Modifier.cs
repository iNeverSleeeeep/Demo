using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.GameLogic.Entities;

namespace Demo.GameLogic.Componnets
{
    class Modifier : Component
    {
        public LinkedList<ModifierData> modifiers = null;

        public Modifier(Entity entity) : base(entity)
        {
            modifiers = new LinkedList<ModifierData>();
        }

        public class ModifierData
        {
            public string name = null;
        }
    }
}
