using Demo.GameLogic.Componnets;
using System.Collections.Generic;

namespace Demo.GameLogic.Systems
{
    class CollideSystem : ITickable
    {
        private List<Collider> m_TempColliders;

        public CollideSystem()
        {
            m_TempColliders = new List<Collider>();
        }

        public void Tick()
        {
            var entityManager = Game.Instance.gameLogicManager.entityManager;
            var entities = entityManager.GetAllEntities();
            m_TempColliders.Clear();
            foreach (var item in entities)
            {
                var entity = item.Value;
                var collider = entity.collider;
                if (collider != null)
                    m_TempColliders.Add(collider);
            }
            for (var i = 0; i < m_TempColliders.Count; ++i)
            {
                var collider1 = m_TempColliders[i];
                for (var j = i + 1; j < m_TempColliders.Count; ++j)
                {
                    var collider2 = m_TempColliders[j];
                    if (collider1.IsCollideWith(collider2))
                    {
                        if (collider1.onCollide != null)
                            collider1.onCollide(collider1.entity, collider2.entity);
                        if (collider2.onCollide != null)
                            collider2.onCollide(collider2.entity, collider1.entity);
                    }
                }
            }
        }
    }
}

