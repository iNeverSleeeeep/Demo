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

        public ModifierContext context { get; set; }

        public IModifierExecutor(List<IAbilityCommand> cmds)
        {
            m_Commands = cmds;
        }

        public abstract void Execute();
        public abstract IModifierExecutor Clone();

        public virtual void Kill()
        {
            var ctx = new AbilityCommandContext();
            foreach (var cmd in m_Commands)
                cmd.Reverse(ctx);
            m_Commands = null;
        }

        protected void ExecuteCommands()
        {
            var ctx = new AbilityCommandContext();
            foreach (var cmd in m_Commands)
                cmd.Execute(ctx);
        }
    }

    class ModifierRoot : IModifierExecutor
    {
        private static List<IAbilityCommand> EmptyList = new List<IAbilityCommand>();
        private List<IModifierExecutor> modifiers;
        private float m_Duration;
        private IEnumerator m_AutoKill;

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

            return root;
        }

        public override void Execute()
        {
            foreach (var modifier in modifiers)
                modifier.Execute();
            if (m_Duration > 0)
            {
                m_AutoKill = AutoKill();
                Game.Instance.coroutineManager.StartLogic(m_AutoKill);
            }
        }

        public IEnumerator AutoKill()
        {
            yield return new WaitForLogicSeconds(m_Duration);
            Kill();
        }

        public override void Kill()
        {
            m_AutoKill = null;
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

        public override void Execute()
        {
            m_IntervalEnumrator = ExecuteInterval(m_Interval);
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

        private IEnumerator ExecuteInterval(float interval)
        {
            while (true)
            {
                ExecuteCommands();
                yield return new WaitForLogicSeconds(interval);
            }
        }
    }
}
