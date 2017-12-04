using Demo.GameLogic.Abilities;
using Demo.GameLogic.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic.Systems
{
    class AbilityContext
    {
        public float startTime;
        public int caster;

        public Dictionary<string, ModifierRoot> modifiers;
        public Dictionary<AbilityEventTrigger, TriggerEvent> triggers;

        public delegate void TriggerEvent();
    }

    abstract class IAbilityExecutor
    {
        protected List<IAbilityCommand> commands { get; set; }

        public IAbilityExecutor(List<IAbilityCommand> cmds)
        {
            commands = cmds;
        }

        public abstract IAbilityExecutor Clone();

        public virtual void Kill()
        {
            commands = null;
        }

        public abstract void Execute(AbilityContext ctx);

        protected void ExecuteCommands(AbilityContext ctx)
        {
            var cmdCtx = new AbilityCommandContext();
            if (commands != null)
            {
                foreach (var cmd in commands)
                    cmd.Execute(cmdCtx);
            }
        }
    }

    class AbilityRoot : IAbilityExecutor
    {
        private readonly static List<IAbilityCommand> EmptyList = new List<IAbilityCommand>();

        private AbilityContext m_Context = null;
        private List<IAbilityExecutor> m_AbilityEvents = null;

        public int caster
        {
            get { return m_Context.caster; }
            set { m_Context.caster = value; }
        }

        public Dictionary<string, ModifierRoot> modifiers
        {
            get { return m_Context.modifiers; }
        }

        public AbilityRoot() : base(EmptyList)
        {
            m_Context = new AbilityContext();
            m_Context.modifiers = new Dictionary<string, ModifierRoot>();
            m_AbilityEvents = new List<IAbilityExecutor>();
        }

        public void AddAbilityEvent(IAbilityExecutor abilityEvent)
        {
            m_AbilityEvents.Add(abilityEvent);
        }

        public void Execute()
        {
            m_Context.startTime = Utils.Time.logicTime;

            Execute(m_Context);
            foreach (var abilityEvent in m_AbilityEvents)
                abilityEvent.Execute(m_Context);
        }

        public override void Kill()
        {
            base.Kill();
            m_Context = null;
        }

        public override IAbilityExecutor Clone()
        {
            var root =  new AbilityRoot();
            foreach (var modifier in modifiers)
                root.modifiers.Add(modifier.Key, modifier.Value);
            foreach (var abilityEvent in m_AbilityEvents)
                root.m_AbilityEvents.Add(abilityEvent.Clone());
            return root;
        }
        
        public override void Execute(AbilityContext ctx)
        {
            ExecuteCommands(ctx);
        }
    }

    class AbilityTimer : IAbilityExecutor
    {
        private float m_Time = 0;
        private IEnumerator m_TimeEnumrator = null;

        public AbilityTimer(List<IAbilityCommand> cmd, float time) : base(cmd)
        {
            m_Time = time;
        }

        public override IAbilityExecutor Clone()
        {
            return new AbilityTimer(commands, m_Time);
        }

        public override void Kill()
        {
            base.Kill();
        }

        public override void Execute(AbilityContext ctx)
        {
            Game.Instance.coroutineManager.StartLogic(ExecuteDelay(ctx));
        }

        private IEnumerator ExecuteDelay(AbilityContext ctx)
        {
            yield return new WaitForLogicSeconds(m_Time);
            ExecuteCommands(ctx);
        }
    }

    class AbilityTirgger : IAbilityExecutor
    {
        private AbilityEventTrigger m_TriggerType;
        private AbilityContext m_Context;

        public AbilityTirgger(List<IAbilityCommand> cmds, AbilityEventTrigger trigger) : base(cmds)
        {
            m_TriggerType = trigger;
        }

        public override void Execute(AbilityContext ctx)
        {
            m_Context = ctx;
            if (ctx.triggers.ContainsKey(m_TriggerType))
                ctx.triggers[m_TriggerType] += ExecuteImpl;
            else
                ctx.triggers.Add(m_TriggerType, ExecuteImpl);
        }

        public override IAbilityExecutor Clone()
        {
            return new AbilityTirgger(commands, m_TriggerType);
        }

        public override void Kill()
        {
            m_Context.triggers[m_TriggerType] -= ExecuteImpl;
            m_Context = null;
            base.Kill();
        }

        private void ExecuteImpl()
        {
            var commandCtx = new AbilityCommandContext();
            foreach (var cmd in commands)
                cmd.Execute(commandCtx);
        }
    }
}


