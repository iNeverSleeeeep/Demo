using Demo.GameLogic.Entities;
using UnityEngine;

namespace Demo.GameLogic.Componnets
{
    class Position : Component
    {
        public Vector3 position { get; set; }

        public Position(Entity entity) : base(entity)
        {

        }
    }
}

