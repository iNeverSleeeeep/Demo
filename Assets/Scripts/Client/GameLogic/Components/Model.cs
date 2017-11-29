using Demo.GameLogic.Entities;
using UnityEngine;

namespace Demo.GameLogic.Componnets
{
    class Model : Component
    {
        Transform m_Transform = null;
        public Transform transform
        {
            get { return m_Transform; }
        }
        public virtual string name
        {
            get { return m_Transform.name; }
            set { m_Transform.name = value; }
        }
        public virtual Vector3 position
        {
            get { return m_Transform.position; }
            set { m_Transform.position = value; }
        }
        public virtual Quaternion rotation
        {
            get { return m_Transform.rotation; }
            set { m_Transform.rotation = value; }
        }
        public virtual Material material
        {
            get { return m_Transform.GetComponent<Renderer>().material; }
        }

        public Model(Entity entity) : base(entity)
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            var capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            m_Transform = capsule.transform;
        }

        protected override void OnDisable()
        {
            GameObject.Destroy(m_Transform.gameObject);
            m_Transform = null;
            base.OnDisable();
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}

