using Demo.GameLogic.Abilities;
using Demo.GameLogic.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic.Systems
{
    class AbilityExecutorContext
    {
        public float startTime;
        public Entity caster;

        public Dictionary<string, IModifierExecutor> modifiers;
        public Dictionary<AbilityEventTrigger, Action<AbilityExecutorContext>> triggers;
    }

    abstract class IAbilityExecutor
    {
        private IAbilityExecutor m_Next = null;
        private List<IAbilityCommand> m_Commands = null;

        protected List<IAbilityCommand> commands { get { return m_Commands; } }
        public IAbilityExecutor next { get { return m_Next; } set { m_Next = value; } }

        public IAbilityExecutor(List<IAbilityCommand> cmds)
        {
            m_Commands = cmds;
        }
        protected virtual IEnumerator Execute(AbilityExecutorContext ctx)
        {
            if (m_Next != null)
                yield return m_Next.Execute(ctx);
        }
        public virtual void Kill()
        {
            if (m_Next != null)
                m_Next.Kill();

            m_Next = null;
            m_Commands = null;
        }

        public abstract IAbilityExecutor Clone();
    }

    class AbilityRoot : IAbilityExecutor
    {
        private readonly static List<IAbilityCommand> EmptyList = new List<IAbilityCommand>();

        private AbilityExecutorContext m_Context = null;
        private IEnumerator m_ExecuteEnumrator = null;

        public Entity caster
        {
            get { return m_Context.caster; }
            set { m_Context.caster = value; }
        }

        public Dictionary<string, IModifierExecutor> modifiers
        {
            get { return m_Context.modifiers; }
        }

        public AbilityRoot() : base(EmptyList)
        {
            m_Context = new AbilityExecutorContext();
            m_Context.modifiers = new Dictionary<string, IModifierExecutor>();
        }

        public void Execute()
        {
            m_ExecuteEnumrator = Execute(m_Context);
            m_Context.startTime = Utils.Time.logicTime;
            Game.Instance.coroutineManager.StartLogic(m_ExecuteEnumrator);
        }

        protected override IEnumerator Execute(AbilityExecutorContext ctx)
        {
            return base.Execute(ctx);
        }

        public override void Kill()
        {
            base.Kill();
            Game.Instance.coroutineManager.StopLogic(m_ExecuteEnumrator);
            m_ExecuteEnumrator = null;
            m_Context = null;
        }

        public override IAbilityExecutor Clone()
        {
            var root =  new AbilityRoot();
            root.next = next.Clone();
            return root;
        }
    }

    class AbilityTimer : IAbilityExecutor
    {
        private float m_Time = 0;
        public AbilityTimer(List<IAbilityCommand> cmd, float time) : base(cmd)
        {
            m_Time = time;
        }

        public override IAbilityExecutor Clone()
        {
            var timer =  new AbilityTimer(commands, m_Time);
            timer.next = next.Clone();
            return timer;
        }

        protected override IEnumerator Execute(AbilityExecutorContext ctx)
        {
            var deltaTime = Utils.Time.logicTime - ctx.startTime;
            if (m_Time > deltaTime)
                yield return new WaitForSeconds(m_Time - deltaTime);
            var commandCtx = new AbilityCommandContext();
            foreach (var cmd in commands)
                cmd.Execute(commandCtx);
            yield return base.Execute(ctx);
        }

        public override void Kill()
        {
            base.Kill();
        }
    }

    class AbilityTirgger : IAbilityExecutor
    {
        private AbilityEventTrigger m_TriggerType;
        private AbilityExecutorContext m_Context;

        public AbilityTirgger(List<IAbilityCommand> cmds, AbilityEventTrigger trigger) : base(cmds)
        {
            m_TriggerType = trigger;
        }

        protected override IEnumerator Execute(AbilityExecutorContext ctx)
        {
            
            yield return base.Execute(ctx);
        }

        public override IAbilityExecutor Clone()
        {
            throw new NotImplementedException();
        }

        private void ExecuteImpl()
        {
            var commandCtx = new AbilityCommandContext();
            foreach (var cmd in commands)
                cmd.Execute(commandCtx);
        }
    }
}


