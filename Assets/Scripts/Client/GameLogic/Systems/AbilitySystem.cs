using System.Collections.Generic;
using Demo.GameLogic.Componnets;
using Demo.GameLogic.Abilities;
using Demo.GameLogic.Events;
using UnityEngine;
using Demo.GameLogic.Entities;

namespace Demo.GameLogic.Systems
{
    partial class AbilitySystem : ITickable
    {
        private Dictionary<string, AbilityRoot> m_CachedAbilities = null;
        private Dictionary<string, AbilityCondition> m_CachedAbilityConditions = null;
        private AbilityParser m_AbilityParse = null;

        public AbilitySystem()
        {
            m_CachedAbilities = new Dictionary<string, AbilityRoot>();
            m_CachedAbilityConditions = new Dictionary<string, AbilityCondition>();
            m_AbilityParse = new AbilityParser();
        }

        public void AddAbility(string path)
        {
            Game.Instance.resourceLoader.Load<TextAsset>(path, asset=>
            {
                var ability = JsonUtility.FromJson<DataDrivenAbility>(asset.text);
                if (m_CachedAbilities.ContainsKey(ability.name) == false)
                {
                    var root = m_AbilityParse.Parse(ability);
                    m_CachedAbilities.Add(ability.name, root);
                    var condition = m_AbilityParse.ParseCondition(ability);
                    m_CachedAbilityConditions.Add(ability.name, condition);
                }
            });
        }

        public void Tick()
        {
            var entityManager = Game.Instance.gameLogicManager.entityManager;
            var entities = entityManager.GetAllEntities();
            foreach (var item in entities)
            {
                var ability = item.Value.ability;
                if (ability != null && ability.current == null)
                {
                    if (ability.abilityToCast != null)
                    {
                        var target = entityManager.GetEntity(ability.target);
                        var status = ConditionStatus(item.Value, target, ability.abilityToCast);
                        if (status == AbilityConditionStatus.Success)
                        {
                            item.Value.movement.speed = 0;
                            CastAbility(ability, ability.abilityToCast, new AbilityTarget(target.id));
                            ability.abilityToCast = null;
                        }
                        else if (status == AbilityConditionStatus.TooFar)
                        {
                            var offset = target.position.position - item.Value.position.position;
                            var angle = Vector3.Angle(offset, Vector3.forward);
                            if (Vector3.Cross(offset, Vector3.forward).y > 0)
                                angle = 360 - angle;
                            item.Value.movement.speed = item.Value.movement.defaultSpeed;
                            item.Value.movement.angle = angle;
                        }
                    }
                }
            }
        }

        protected AbilityConditionStatus ConditionStatus(Entity caster, Entity target, string abilityName)
        {
            AbilityCondition condition;
            if (m_CachedAbilityConditions.TryGetValue(abilityName, out condition))
            {
                if (Vector3.Distance(caster.position.position, target.position.position) > condition.castRange)
                    return AbilityConditionStatus.TooFar;
                return AbilityConditionStatus.Success;
            }
            return AbilityConditionStatus.Invalid;
        }

        public void CastAbility(Ability ability, string abilityName, AbilityTarget target)
        {
            if (m_CachedAbilities.ContainsKey(abilityName) == false)
                return;
            var abilityData = new Ability.AbilityData()
            {
                name = abilityName,
                startTime = Utils.Time.logicTime,
                root = m_CachedAbilities[abilityName].Clone() as AbilityRoot
            };
            ability.current = abilityData;
            ability.current.root.caster = ability.entity.id;
            ability.current.root.target = target;
            ability.current.root.Execute();
            LogicEvent.RaiseEvent(LogicEventType.CastAbility, ability.entity);
        }

        protected enum AbilityConditionStatus
        {
            Invalid,
            Success,
            NoMana,
            TooFar,
        }
    }
}
