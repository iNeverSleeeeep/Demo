using UnityEngine;
using UnityEditor;
using Demo.GameLogic.Entities;

namespace Demo.GameLogic.Systems
{
    class TrackingSystem : ITickable
    {
        public void Tick()
        {
            var entityManager = Game.Instance.gameLogicManager.entityManager;
            var entities = entityManager.GetAllEntities();
            foreach (var item in entities)
            {
                var tracker = item.Value.tracker;
                if (tracker == null)
                    continue;

                var target = entityManager.GetEntity(tracker.target);
                if (target == null)
                {
                    if (tracker.destination == item.Value.position.position)
                    {
                        Entity.Destroy(item.Value);
                    }
                    continue;
                }

                tracker.destination = target.position.position;
                var angle = Utils.Angle.Between(item.Value.position.position, target.position.position);
                item.Value.movement.angle = angle;
            }
        }
    }
}
