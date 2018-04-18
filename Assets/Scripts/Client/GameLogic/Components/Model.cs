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
        private string m_NameCache;
        public virtual string name
        {
            get 
            {
                return m_NameCache; 
            }
            set 
            {
                if (transform)
                    transform.name = value; 
                m_NameCache = value;    
            }
        }
        private Vector3 m_PositionCache;
        public virtual Vector3 position
        {
            get 
            { 
                return m_PositionCache; 
            }
            set 
            { 
                if (transform)
                    transform.position = value; 
                m_PositionCache = value;
            }
        }
        private Quaternion m_RotationCache;
        public virtual Quaternion rotation
        {
            get 
            { 
                return m_RotationCache; 
            }
            set 
            { 
                if (transform)
                    transform.rotation = value; 
                m_RotationCache = value;
            }
        }

        private Animator m_AnimatorCache;
        public virtual Animator animator
        {
            get
            {
                if (m_AnimatorCache)
                    return m_AnimatorCache;
                return m_AnimatorCache = transform.GetComponentInChildren<Animator>(); 
            }
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
            Game.Instance.resourceLoader.Load<GameObject>("Prefabs/Player", OnModelLoadFinish);
            m_Transform = new GameObject().transform;
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

        protected void OnModelLoadFinish(GameObject go)
        {
            var trans = GameObject.Instantiate(go, position, rotation).transform;
            trans.SetParent(m_Transform);
        }
    }
}

