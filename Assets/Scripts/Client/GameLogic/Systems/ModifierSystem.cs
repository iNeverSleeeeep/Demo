using UnityEngine;

namespace Demo.GameLogic.Systems
{
    class ModifierSystem : ITickable
    {
        public void Tick()
        {
            var entityManager = Game.Instance.gameLogicManager.entityManager;
            var entities = entityManager.GetAllEntities();
            foreach (var item in entities)
            {

            }
        }
    }
}

