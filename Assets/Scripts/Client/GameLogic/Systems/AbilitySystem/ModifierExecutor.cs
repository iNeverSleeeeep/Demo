using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic.Systems
{
    class ModifierContext
    {
        public int caster;
        public int owner;
    }

    abstract class IModifierExecutor
    {
        private List<IAbilityCommand> m_Commands = null;
        protected List<IAbilityCommand> commands { get { return m_Commands; } }

        public IModifierExecutor(List<IAbilityCommand> cmds)
        {
            m_Commands = cmds;
        }

        public abstract void Execute(ModifierContext context);
        public abstract IModifierExecutor Clone();

        public virtual void Kill()
        {
            m_Commands = null;
        }

        protected void ExecuteCommands(ModifierContext context)
        {
            if (m_Commands != null)
            {
                var ctx = new AbilityCommandContext
                {
                    caster = context.caster,
                    target = context.owner
                };
                foreach (var cmd in m_Commands)
                    cmd.Execute(ctx);
            }
        }
    }

    class ModifierRoot : IModifierExecutor
    {
        private static List<IAbilityCommand> EmptyList = new List<IAbilityCommand>();
        private List<IModifierExecutor> modifiers;
        private float m_Duration;
        private IEnumerator m_AutoKill;
        
        public ModifierContext context { get; set; }

        public ModifierRoot(float duration) : base(EmptyList)
        {
            modifiers = new List<IModifierExecutor>();
            m_Duration = duration;
        }

        public void AddModifier(IModifierExecutor modifier)
        {
            modifiers.Add(modifier);
        }

        public override IModifierExecutor Clone()
        {
            var root = new ModifierRoot(m_Duration);
            foreach (var modifier in modifiers)
                root.AddModifier(modifier.Clone());
            return root;
        }

        public void Execute()
        {
            Execute(context);
        }

        public override void Execute(ModifierContext context)
        {
            foreach (var modifier in modifiers)
                modifier.Execute(context);
            if (m_Duration > 0)
            {
                m_AutoKill = AutoKill();
                Game.Instance.coroutineManager.StartLogic(m_AutoKill);
            }
        }

        public IEnumerator AutoKill()
        {
            yield return new WaitForLogicSeconds(m_Duration);
            m_AutoKill = null;
            Kill();
        }

        public override void Kill()
        {
            if (m_AutoKill != null)
            {
                Game.Instance.coroutineManager.StopLogic(m_AutoKill);
                m_AutoKill = null; 
            }
            foreach (var modifier in modifiers)
                modifier.Kill();
            var entity = Game.Instance.gameLogicManager.entityManager.GetEntity(context.owner);
            if (entity != null)
                entity.modifier.RemoveModifier(this);
            base.Kill();
        }
    }

    class ModifierThinkInterval : IModifierExecutor
    {
        private float m_Interval;
        private IEnumerator m_IntervalEnumrator;

        public ModifierThinkInterval(List<IAbilityCommand> cmds, float interval) : base(cmds)
        {
            m_Interval = interval;
        }

        public override void Execute(ModifierContext context)
        {
            m_IntervalEnumrator = ExecuteInterval(m_Interval, context);
            Game.Instance.coroutineManager.StartLogic(m_IntervalEnumrator);
        }

        public override void Kill()
        {
            Game.Instance.coroutineManager.StopLogic(m_IntervalEnumrator);
            m_IntervalEnumrator = null;
            base.Kill();
        }

        public override IModifierExecutor Clone()
        {
            return new ModifierThinkInterval(commands, m_Interval);
        }

        private IEnumerator ExecuteInterval(float interval, ModifierContext context)
        {
            while (true)
            {
                ExecuteCommands(context);
                yield return new WaitForLogicSeconds(interval);
            }
        }
    }
}
