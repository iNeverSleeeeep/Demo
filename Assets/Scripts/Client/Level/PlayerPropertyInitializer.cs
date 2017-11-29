using Demo.GameLogic.Componnets;

namespace Demo.Level
{
    static class PlayerPropertyInitializer
    {
        public static void Handle(Property property)
        {
            property.attack = 10;
            property.maxHp = 100;
            property.maxMana = 100;
            property.hp = property.maxHp;
            property.mana = property.maxMana;
        }
    }
}

