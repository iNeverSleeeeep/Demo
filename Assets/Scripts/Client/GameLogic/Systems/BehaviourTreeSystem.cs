
using Demo.Frame;
using Demo.GameLogic.Componnets;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic.Systems
{
    class BehaviourTreeSystem : ITickable, IFrameDataProvider
    {
        List<FrameData.EntityData> m_EntityData = null;

        public BehaviourTreeSystem()
        {
            m_EntityData = new List<FrameData.EntityData>();
        }

        public void Tick()
        {
            var entityManager = Game.Instance.gameLogicManager.entityManager;
            var entities = entityManager.GetAllEntities();
            foreach (var item in entities)
            {
                var entity = item.Value;
                var brain = entity.GetComponent<Brain>();
                if (brain != null)
                {
                    var enemy = entityManager.GetEntity(brain.enemy);
                    var offset = enemy.position.position - entity.position.position;
                    var distance = offset.magnitude;
                    if (distance < 5)
                    {
                        var angle = Vector3.Angle(offset, Vector3.forward);
                        if (Vector3.Cross(offset, Vector3.forward).y > 0)
                            angle = 360 - angle;
                        var entityData = new FrameData.EntityData()
                        {
                            id = entity.id,
                            type = FrameData.OperationType.Moveing,
                            angle = angle
                        };
                        m_EntityData.Add(entityData);
                    }
                    else if (entity.movement.speed > 0)
                    {
                        var entityData = new FrameData.EntityData()
                        {
                            id = entity.id,
                            type = FrameData.OperationType.EndMove,
                            angle = float.NaN
                        };
                        m_EntityData.Add(entityData);
                    }
                }
            }
        }

        public bool GetFrameData(ref FrameData data)
        {
            if (m_EntityData.Count > 0)
            {
                data.data = m_EntityData;
                data.id = Utils.Time.logicFrameCount;
                m_EntityData = new List<FrameData.EntityData>();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

