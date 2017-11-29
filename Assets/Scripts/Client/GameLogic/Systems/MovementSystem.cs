
using Demo.GameLogic.Componnets;
using UnityEngine;

namespace Demo.GameLogic.Systems
{
    class MovementSystem : ITickable
    {
        public void Tick()
        {
            var entities = Game.Instance.gameLogicManager.entityManager.GetAllEntities();
            foreach (var item in entities)
            {
                var entity = item.Value;
                if (entity.movement != null && entity.movement.speed > 0)
                {
                    var quaternion = Quaternion.AngleAxis(entity.movement.angle, Vector3.up);
                    var delta = quaternion * Vector3.forward * entity.movement.speed * Utils.Time.logicDeltaTime; ;
                    var nextPosition = entity.position.position + delta;

                    entity.position.position = nextPosition;

                    var model = entity.model;
                    if (model != null)
                        model.position = nextPosition;

                    var collider = entity.collider;
                    if (collider != null)
                        TestIfCollide(collider);
                }
            }
        }

        public void TestIfCollide(Componnets.Collider collider)
        {
            var entities = Game.Instance.gameLogicManager.entityManager.GetAllEntities();
            foreach (var item in entities)
            {
                if (item.Value == collider.entity) continue;
                var other = item.Value.collider;
                if (collider.IsCollideWith(other) && collider.onCollide != null)
                    collider.onCollide(collider.entity, other.entity);
            }
        }
    }
}

