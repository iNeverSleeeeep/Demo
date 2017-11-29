
using Demo.GameLogic.Componnets;
using UnityEngine;

namespace Demo.GameLogic.Systems
{
    class MovementSystem : ITickable
    {
        public void Tick()
        {
            var entities = Game.Instance.gameLogicManager.entityManager.GetAllEntities();
            foreach(var item in entities)
            {
                var entity = item.Value;
                if (entity.movement != null && entity.movement.speed > 0)
                {
                    var quaternion = Quaternion.AngleAxis(entity.movement.angle, Vector3.up);
                    entity.position.position += quaternion*Vector3.forward*entity.movement.speed*Utils.Time.logicDeltaTime;

                    var model = entity.model;
                    if (model != null)
                        model.position = entity.position.position;
                }
            }
        }
    }
}

