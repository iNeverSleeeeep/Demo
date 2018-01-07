
using Demo.GameLogic.Componnets;
using System.Collections.Generic;

namespace Demo.GameLogic.Entities
{
    sealed class Entity
    {
        readonly int m_Id = 0;
        public int id { get { return m_Id; } }

        private bool m_Active = false;
        public bool active
        {
            get
            {
                return m_Active;
            }
            set
            {
                if (m_Active == value)
                    return;
                m_Active = value;
                foreach (var component in m_Componnets)
                    component.enabled = m_Active;
            }
        }

        List<Component> m_Componnets = null;
        public T GetComponent<T>() where T : Component
        {
            foreach(var component in m_Componnets)
            {
                if (component is T)
                    return component as T;
            }
            return null;
        }
        public void AddComponent<T>(T component) where T : Component
        {
            m_Componnets.Add(component);
        }

        public List<Component> GetAllComponents()
        {
            return m_Componnets;
        }

        public Entity(int id)
        {
            m_Id = id;
            m_Componnets = new List<Component>();
        }

        public override string ToString()
        {
            return id.ToString();
        }

        public static Entity Create(EntityType type)
        {
            return EntityFactory.Create(type);
        }

        public static void Destroy(Entity entity)
        {
            foreach (var component in entity.GetAllComponents())
                component.Destroy();
            Game.Instance.gameLogicManager.entityManager.RemoveEntity(entity.id);
        }

        #region Some Cache to Make GetComponent Fast
        Position m_CachedPosition = null;
        public Position position
        {
            get
            {
                if (m_CachedPosition == null)
                    m_CachedPosition = GetComponent<Position>();
                return m_CachedPosition;
            }
        }
        Movement m_CachedMovement = null;
        public Movement movement
        {
            get
            {
                if (m_CachedMovement == null)
                    m_CachedMovement = GetComponent<Movement>();
                return m_CachedMovement;
            }
        }
        Ability m_CachedAbility = null;
        public Ability ability
        {
            get
            {
                if (m_CachedAbility == null)
                    m_CachedAbility = GetComponent<Ability>();
                return m_CachedAbility;
            }
        }
        Model m_CachedModel = null;
        public Model model
        {
            get
            {
                if (m_CachedModel == null)
                    m_CachedModel = GetComponent<Model>();
                return m_CachedModel;
            }
        }
        Clickable m_CachedClickable = null;
        public Clickable clickable
        {
            get
            {
                if (m_CachedClickable == null)
                    m_CachedClickable = GetComponent<Clickable>();
                return m_CachedClickable;
            }
        }
        Modifier m_CachedModifier = null;
        public Modifier modifier
        {
            get
            {
                if (m_CachedModifier == null)
                    m_CachedModifier = GetComponent<Modifier>();
                return m_CachedModifier;
            }
        }
        Property m_CachedProperty = null;
        public Property property
        {
            get
            {
                if (m_CachedProperty == null)
                    m_CachedProperty = GetComponent<Property>();
                return m_CachedProperty;
            }
        }
        Collider m_CachedCollider = null;
        public Collider collider
        {
            get
            {
                if (m_CachedCollider == null)
                    m_CachedCollider = GetComponent<Collider>();
                return m_CachedCollider;
            }
        }
        Tracker m_CachedTracker = null;
        public Tracker tracker
        {
            get
            {
                if (m_CachedTracker == null)
                    m_CachedTracker = GetComponent<Tracker>();
                return m_CachedTracker;
            }
        }
        Camp m_CachedCamp = null;
        public Camp camp
        {
            get
            {
                if (m_CachedCamp == null)
                    m_CachedCamp = GetComponent<Camp>();
                return m_CachedCamp;
            }
        }
        public void ClearCache()
        {
            m_CachedPosition = null;
            m_CachedMovement = null;
            m_CachedModel = null;
            m_CachedAbility = null;
            m_CachedCollider = null;
            m_CachedProperty = null;
            m_CachedModifier = null;
            m_CachedClickable = null;
            m_CachedTracker = null;
            m_CachedCamp = null;
        }
        #endregion

    }
}

