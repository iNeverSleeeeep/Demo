using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private AbilityParser m_AbilityParse = null;

        public AbilitySystem()
        {
            m_CachedAbilities = new Dictionary<string, AbilityRoot>();
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
                }
            });
        }

        public void Tick()
        {
            var entities = Game.Instance.gameLogicManager.entityManager.GetAllEntities();
            foreach (var item in entities)
            {
                var ability = item.Value.ability;
                if (ability != null)
                {
                    if (ability.abilitiesToCast.Count > 0)
                        CastAbility(ability, ability.abilitiesToCast.Dequeue());
                }
            }
        }

        public void CastAbility(Ability ability, string abilityName)
        {
            var abilityData = new Ability.AbilityData()
            {
                name = abilityName,
            };
            ability.current = abilityData;
            LogicEvent.RaiseEvent(LogicEventType.CastAbility, ability.entity);
        }
    }
}
