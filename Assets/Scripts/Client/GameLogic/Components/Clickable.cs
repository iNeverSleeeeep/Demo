using UnityEngine;
using UnityEditor;
using Demo.GameLogic.Entities;

namespace Demo.GameLogic.Componnets
{
    class Clickable : Component
    {
        public Clickable(Entity entity) : base(entity)
        {

        }

        public ClickableBehaviour.PointerClickEvent clickCallback
        {
            get
            {
                if (entity.model != null && entity.model.transform != null)
                    return entity.model.transform.GetComponent<ClickableBehaviour>().clickCallback;
                return null;
            }
            set
            {
                if (entity.model != null && entity.model.transform != null)
                    entity.model.transform.GetComponent<ClickableBehaviour>().clickCallback = value;
            }
        }


        protected override void OnEnable()
        {
            base.OnEnable();
            if (entity.model != null && entity.model.transform != null)
            {
                var clickable = entity.model.transform.GetComponent<ClickableBehaviour>();
                if (clickable == null)
                    clickable = entity.model.transform.gameObject.AddComponent<ClickableBehaviour>();
                clickable.userData = entity;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (entity.model != null && entity.model.transform != null)
            {
                var clickable = entity.model.transform.GetComponent<ClickableBehaviour>();
                if (clickable != null)
                    GameObject.Destroy(clickable);
            }
        }
    }
}

