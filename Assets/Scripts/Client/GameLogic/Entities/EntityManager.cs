
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic.Entities
{
    class EntityManager
    {
        Dictionary<int, Entity> m_Entities = null;

        public EntityManager()
        {
            m_Entities = new Dictionary<int, Entity>();
        }

        public void AddEntity(Entity entity)
        {
            Debug.Assert(m_Entities.ContainsKey(entity.id) == false);
            m_Entities.Add(entity.id, entity);
        }

        public void RemoveEntity(int entityId)
        {
            Debug.Assert(m_Entities.ContainsKey(entityId));
            m_Entities.Remove(entityId);
        }

        public Entity GetEntity(int entityId)
        {
            Entity entity = null;
            if (m_Entities.TryGetValue(entityId, out entity))
                return entity;
            return null;
        }

        public Dictionary<int, Entity> GetAllEntities()
        {
            return m_Entities;
        }
    }
}

