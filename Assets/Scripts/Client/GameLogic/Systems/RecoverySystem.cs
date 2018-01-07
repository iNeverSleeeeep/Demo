
namespace Demo.GameLogic.Systems
{
    class RecoverySystem : ITickable
    {
        public void Tick()
        {
            var entities = Game.Instance.gameLogicManager.entityManager.GetAllEntities();
            foreach (var item in entities)
            {
                var entity = item.Value;
                if (entity.property == null)
                    return;

                LogicHelper.DoRecoverHp(entity.property.recoveryHp * Utils.Time.logicDeltaTime, entity);
                LogicHelper.DoRecoverMana(entity.property.recoveryMana * Utils.Time.logicDeltaTime, entity);
            }
        }
    }
}