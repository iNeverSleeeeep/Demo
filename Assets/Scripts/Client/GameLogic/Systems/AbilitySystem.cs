using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.GameLogic.Componnets;
using Demo.GameLogic.Abilities;
using Demo.GameLogic.Events;

namespace Demo.GameLogic.Systems
{
    partial class AbilitySystem : ITickable
    {
        private Dictionary<string, DataDrivenAbility> m_CachedAbilities = null;

        public void Tick()
        {
            var entities = Game.Instance.gameLogicManager.entityManager.GetAllEntities();
            foreach (var item in entities)
            {
                var ability = item.Value.ability;
                if (ability != null)
                {
                    if (ability.current != null)
                        TickOneAbility(ability.current);
                    else if (ability.abilitiesToCast.Count > 0)
                        CastAbility(ability, ability.abilitiesToCast.Dequeue());
                }
            }
        }

        public void TickOneAbility(Ability.AbilityData data)
        {
            TickOneAbilityInner(data);
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
