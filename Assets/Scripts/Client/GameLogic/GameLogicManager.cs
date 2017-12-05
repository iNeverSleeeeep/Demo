
using Demo.Frame;
using Demo.GameLogic.Componnets;
using Demo.GameLogic.Entities;
using Demo.GameLogic.Systems;
using UnityEngine;

namespace Demo.GameLogic
{
    sealed class GameLogicManager
    {
        LogicTickManager m_LogicTickManager = null;
        AbilitySystem m_AbilitySystem = null;
        public AbilitySystem abilitySystem { get { return m_AbilitySystem; } }
        EntityManager m_EntityManager = null;
        public EntityManager entityManager { get { return m_EntityManager; } }

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
                        ability.abilityToCast = entityData.abilityName;
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

            var btSystem = new BehaviourTreeSystem();
            m_LogicTickManager.AddTickable(btSystem);

            m_AbilitySystem = new AbilitySystem();
            m_LogicTickManager.AddTickable(m_AbilitySystem);

            Game.Instance.frameDataCollector.AddProvider(btSystem);

            m_EntityManager = new EntityManager();
        }
    }
}

