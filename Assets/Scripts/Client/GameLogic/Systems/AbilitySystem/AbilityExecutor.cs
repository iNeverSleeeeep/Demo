using Demo.GameLogic.Abilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic.Systems
{
    struct AbilityTarget
    {
        public int entity;
        public Vector3 point;

        public AbilityTarget(int entity)
        {
            this.entity = entity;
            point = default(Vector3);
        }

        public AbilityTarget(Vector3 point)
        {
            this.point = point;
            entity = 0;
        }
    }

    class AbilityContext
    {
        public float startTime;
        public int caster;

        public AbilityTarget target;
        public int[] targets;

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
            cmdCtx.caster = ctx.caster;
            cmdCtx.targets = ctx.targets;
            cmdCtx.modifiers = ctx.modifiers;
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
        private IEnumerator m_AutoFinish = null;

        public int caster
        {
            get { return m_Context.caster; }
            set { m_Context.caster = value; }
        }

        public Dictionary<string, ModifierRoot> modifiers
        {
            get { return m_Context.modifiers; }
        }

        public AbilityTarget target
        {
            get { return m_Context.target; }
            set { m_Context.target = value; }
        }
        
        public float keepTime
        {
            get;
            private set;
        }

        public AbilityRoot(float keepTime) : base(EmptyList)
        {
            this.keepTime = keepTime;
            m_Context = new AbilityContext
            {
                modifiers = new Dictionary<string, ModifierRoot>()
            };
            m_AbilityEvents = new List<IAbilityExecutor>();
        }

        public void AddAbilityEvent(IAbilityExecutor abilityEvent)
        {
            m_AbilityEvents.Add(abilityEvent);
        }

        public void Execute()
        {
            m_Context.startTime = Utils.Time.logicTime;

            m_Context.targets = new int[] { m_Context.target.entity };

            Execute(m_Context);
            foreach (var abilityEvent in m_AbilityEvents)
                abilityEvent.Execute(m_Context);

            if (keepTime > 0)
            {
                m_AutoFinish = AutoFinish();
                Game.Instance.coroutineManager.StartLogic(m_AutoFinish);
            }
        }

        public override void Kill()
        {
            if (m_AutoFinish != null)
            {
                Game.Instance.coroutineManager.StopLogic(m_AutoFinish);
                m_AutoFinish = null;
            }
            foreach (var abilityEvent in m_AbilityEvents)
                abilityEvent.Kill();
            m_Context = null;
            base.Kill();
        }

        public override IAbilityExecutor Clone()
        {
            var root =  new AbilityRoot(keepTime);
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

        private void Finish()
        {
            var caster = Game.Instance.gameLogicManager.entityManager.GetEntity(m_Context.caster);
            if (caster != null && caster.ability.current != null && 
                caster.ability.current.root == this)
                caster.ability.current = null;
        }

        private IEnumerator AutoFinish()
        {
            yield return new WaitForLogicSeconds(keepTime);
            m_AutoFinish = null;
            Finish();
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
            if (m_TimeEnumrator != null)
            {
                Game.Instance.coroutineManager.StopLogic(m_TimeEnumrator);
                m_TimeEnumrator = null;
            }
            base.Kill();
        }

        public override void Execute(AbilityContext ctx)
        {
            m_TimeEnumrator = ExecuteDelay(ctx);
            Game.Instance.coroutineManager.StartLogic(m_TimeEnumrator);
        }

        private IEnumerator ExecuteDelay(AbilityContext ctx)
        {
            yield return new WaitForLogicSeconds(m_Time);
            m_TimeEnumrator = null;
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


