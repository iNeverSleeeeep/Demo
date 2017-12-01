using Demo.GameLogic.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.GameLogic.Systems
{
    class ModifierContext
    {
        public Entity caster;
        public Entity owner;
    }

    abstract class IModifierExecutor
    {
        private List<IAbilityCommand> m_Commands = null;
        protected List<IAbilityCommand> commands { get { return m_Commands; } }

        public ModifierContext context { get; set; }

        public IModifierExecutor(List<IAbilityCommand> cmds)
        {
            m_Commands = cmds;
        }

        public abstract IEnumerator Execute();
        public abstract IModifierExecutor Clone();

        public virtual void Kill()
        {
            var ctx = new AbilityCommandContext();
            foreach (var cmd in m_Commands)
                cmd.Reverse(ctx);
            m_Commands = null;
        }

        protected void ExecuteImpl()
        {
            var ctx = new AbilityCommandContext();
            foreach (var cmd in m_Commands)
                cmd.Execute(ctx);
        }
    }
}
