
namespace Demo.Level
{
    class WarlockWarsLevelFactory : LevelFactory
    {
        public override LevelFinisher GetLevelFinisher()
        {
            return new WarlockWarsLevelFinisher();
        }

        public override LevelLoader GetLevelLoader()
        {
            return new WarlockWarsLevelLoader();
        }
    }
}

