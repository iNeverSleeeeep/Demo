using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.GameLogic.Entities;

namespace Demo.GameLogic.Componnets
{
    class Collider : Component
    {
        public CollideEvent onCollide = null;

        public float size { get; set; }

        public Collider(Entity entity) : base(entity)
        {

        }

        public bool IsCollideWith(Collider other)
        {
            var sum2 = size * size + other.size * other.size;
            var dis = other.entity.position.position - entity.position.position;
            return sum2 > dis.sqrMagnitude;
        }

        public delegate void CollideEvent(Entity moved, Entity other);
    }
}
