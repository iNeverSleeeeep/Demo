
using Demo.GameLogic.Entities;

namespace Demo.GameLogic.Componnets
{
    class Camp : Component
    {
        public enum Type
        {
            Invalid,
            Camp1,
            Camp2,
            Camp3,
            Camp4,
            Camp5,
            Camp6,
            Camp7,
            Camp8,
            Camp9,
            Camp10,
        }

        public Type type
        {
            get; set;
        }

        public Camp(Entity entity) : base(entity)
        {

        }
    }
}