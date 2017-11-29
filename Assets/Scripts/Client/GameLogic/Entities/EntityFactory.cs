
using Demo.GameLogic.Componnets;

namespace Demo.GameLogic.Entities
{
    static class EntityFactory
    {
        static class IDProvider
        {
            static int Next = 1;
            public static int Get() { return Next++; }
        }

        public enum EntityType
        {
            Player,
            Computer,
        }

        public static Entity CreateEntity(EntityType type)
        {
            var entity = new Entity(IDProvider.Get());
            switch (type)
            {
                case EntityType.Player:
                    entity.AddComponent(new Position(entity));
                    entity.AddComponent(new Movement(entity));
                    entity.AddComponent(new Property(entity));
                    entity.AddComponent(new PlayerModel(entity));
                    break;
                case EntityType.Computer:
                    entity.AddComponent(new Position(entity));
                    entity.AddComponent(new Movement(entity));
                    entity.AddComponent(new Property(entity));
                    entity.AddComponent(new Brain(entity));
                    entity.AddComponent(new PlayerModel(entity));
                    break;
            }
            
            return entity;
        }
    }
}

