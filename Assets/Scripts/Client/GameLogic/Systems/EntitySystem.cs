
using Demo.GameLogic.Entities;
using System.Collections.Generic;

namespace Demo.GameLogic.Systems
{
    class EntitySystem : ITickable
    {
        private List<Entity> m_DeadPool = new List<Entity>();

        public void Tick()
        {
            var entities = Game.Instance.gameLogicManager.entityManager.GetAllEntities();
            foreach(var item in entities)
            {
                if (item.Value.property != null && item.Value.property.hp <= 0)
                    m_DeadPool.Add(item.Value);
            }
            foreach (var entity in m_DeadPool)
            {
                foreach(var component in entity.GetAllComponents())
                    component.Destroy();
                Game.Instance.gameLogicManager.entityManager.RemoveEntity(entity.id);
            }
            m_DeadPool.Clear();
        }
    }
}

