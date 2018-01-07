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
                if (entity.movement != null && entity.movement.speed > 0 && float.IsNaN(entity.movement.angle) == false)
                {
                    var quaternion = Quaternion.AngleAxis(entity.movement.angle, Vector3.up);
                    var delta = quaternion * Vector3.forward * entity.movement.speed * Utils.Time.logicDeltaTime; ;
                    var nextPosition = entity.position.position + delta;

                    entity.position.position = nextPosition;

                    // TODO 20180107 移动的逻辑和表现要分开，不然会显得卡
                    var model = entity.model;
                    if (model != null)
                        model.position = nextPosition;
                }
            }
        }
    }
}

