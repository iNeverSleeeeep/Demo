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

        public enum Layer
        {
            Hero = 1 << 0,
            Projectile = 1 << 1
        }
        
        public int selfLayer { private get; set; }
        public int maskLayer { private get; set; }

        public Collider(Entity entity) : base(entity)
        {

        }

        public bool IsCollideWith(Collider other)
        {
            if ((selfLayer & other.maskLayer) == 0 && (maskLayer & other.selfLayer) == 0)
                return false;
            var sum2 = size * size + other.size * other.size;
            var dis = other.entity.position.position - entity.position.position;
            return sum2 > dis.sqrMagnitude;
        }

        public delegate void CollideEvent(Entity self, Entity other);
    }
}
