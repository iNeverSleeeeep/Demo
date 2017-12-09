
using Demo.Frame;
using Demo.GameLogic.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo.Input
{
    abstract class InputHandler : IFrameDataProvider
    {
        protected List<FrameData.EntityData> m_FrameData = null;

        public InputHandler()
        {
            m_FrameData = new List<FrameData.EntityData>();
        }
        
        public void OnEntityClick(PointerEventData eventData, object userData)
        {
            var target = userData as Entity;
            if (target.id == Game.Instance.inputManager.entity)
                return;

            var data = new FrameData.EntityData()
            {
                id = Game.Instance.inputManager.entity,
                type = FrameData.OperationType.Ability,
                abilityName = "TestAbility",
                target = target.id
            };
            m_FrameData.Add(data);
        }

        public abstract void Tick();

        public bool GetFrameData(ref FrameData data)
        {
            if (m_FrameData.Count > 0)
            {
                data.id = Utils.Time.logicFrameCount;
                data.data = m_FrameData;
                m_FrameData = new List<FrameData.EntityData>();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

