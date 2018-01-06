
using Demo.GameLogic.Componnets;

namespace Demo.GameLogic.Entities
{
    enum EntityType
    {
        Player,
        Computer,
        TrackingProjectile,
        LinearProjectile,
    }

    static class EntityFactory
    {
        static class IDProvider
        {
            static int Next = 1;
            public static int Get() { return Next++; }
        }

        public static Entity Create(EntityType type)
        {
            var entity = new Entity(IDProvider.Get());
            switch (type)
            {
                case EntityType.Player:
                    entity.AddComponent(new Position(entity));
                    entity.AddComponent(new Movement(entity));
                    entity.AddComponent(new Property(entity));
                    entity.AddComponent(new PlayerModel(entity));
                    entity.AddComponent(new Ability(entity));
                    entity.AddComponent(new Modifier(entity));
                    entity.AddComponent(new Collider(entity));
                    entity.AddComponent(new Clickable(entity));
                    break;
                case EntityType.Computer:
                    entity.AddComponent(new Position(entity));
                    entity.AddComponent(new Movement(entity));
                    entity.AddComponent(new Property(entity));
                    entity.AddComponent(new Brain(entity));
                    entity.AddComponent(new PlayerModel(entity));
                    entity.AddComponent(new Ability(entity));
                    entity.AddComponent(new Modifier(entity));
                    entity.AddComponent(new Collider(entity));
                    entity.AddComponent(new Clickable(entity));
                    break;
                case EntityType.LinearProjectile:
                    entity.AddComponent(new Position(entity));
                    entity.AddComponent(new Movement(entity));
                    entity.AddComponent(new Model(entity));
                    entity.AddComponent(new Collider(entity));
                    break;
                case EntityType.TrackingProjectile:
                    entity.AddComponent(new Position(entity));
                    entity.AddComponent(new Movement(entity));
                    entity.AddComponent(new Model(entity));
                    entity.AddComponent(new Tracker(entity));
                    entity.AddComponent(new Collider(entity));
                    break;
            }
            
            return entity;
        }
    }
}

