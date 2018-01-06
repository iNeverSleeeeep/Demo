using Demo.GameLogic.Entities;

namespace Demo.GameLogic.Componnets
{
    class Tracker : Component
    {
        public int target { get; set; }
        public UnityEngine.Vector3 destination { get; set; }

        public Tracker(Entity entity) : base(entity)
        {
        }
    }
}
