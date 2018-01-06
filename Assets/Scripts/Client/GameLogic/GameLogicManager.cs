
using Demo.Frame;
using Demo.GameLogic.Componnets;
using Demo.GameLogic.Entities;
using Demo.GameLogic.Systems;
using UnityEngine;

namespace Demo.GameLogic
{
    sealed class GameLogicManager
    {
        private LogicTickManager m_LogicTickManager = null;
        private AbilitySystem m_AbilitySystem = null;
        public AbilitySystem abilitySystem { get { return m_AbilitySystem; } }
        private ModifierSystem m_ModifierSystem = null;
        public ModifierSystem modifierSystem { get { return m_ModifierSystem; } }
        private EntityManager m_EntityManager = null;
        public EntityManager entityManager { get { return m_EntityManager; } }
        private EntitySystem m_EntitySystem = null;
        public EntitySystem entitySystem { get { return m_EntitySystem; } }

        public void HandleFrameData(FrameData data)
        {
            if (data.data == null) return;
            foreach (var entityData in data.data)
            {
                var entity = m_EntityManager.GetEntity(entityData.id);
                Debug.Assert(entity != null);
                if (entityData.type == FrameData.OperationType.Moveing)
                {
                    var movement = entity.movement;
                    if (movement != null)
                    {
                        movement.speed = movement.defaultSpeed;
                        movement.angle = entityData.angle;
                    }
                    var ability = entity.ability;
                    if (ability != null && ability.abilityToCast != null)
                    {
                        ability.abilityToCast = null;
                        ability.target = 0;
                    }
                }
                else if (entityData.type == FrameData.OperationType.EndMove)
                {
                    var movement = entity.movement;
                    if (movement != null)
                    {
                        movement.speed = 0;
                        movement.angle = float.NaN;
                    }
                }
                else if (entityData.type == FrameData.OperationType.Ability)
                {
                    var ability = entity.ability;
                    if (ability != null)
                    {
                        ability.abilityToCast = entityData.abilityName;
                        ability.target = entityData.target;
                    }
                }
            }
        }

        public void Tick()
        {
            m_LogicTickManager.Tick();
        }

        public GameLogicManager()
        {
            m_LogicTickManager = new LogicTickManager();
            m_LogicTickManager.AddTickable(new MovementSystem());
            m_LogicTickManager.AddTickable(new CollideSystem());
            m_LogicTickManager.AddTickable(new TrackingSystem());

            var btSystem = new BehaviourTreeSystem();
            m_LogicTickManager.AddTickable(btSystem);

            m_AbilitySystem = new AbilitySystem();
            m_LogicTickManager.AddTickable(m_AbilitySystem);

            m_ModifierSystem = new ModifierSystem();
            m_LogicTickManager.AddTickable(m_ModifierSystem);

            m_EntitySystem = new EntitySystem();
            m_LogicTickManager.AddTickable(m_EntitySystem);

            Game.Instance.frameDataCollector.AddProvider(btSystem);

            m_EntityManager = new EntityManager();
        }
    }
}

