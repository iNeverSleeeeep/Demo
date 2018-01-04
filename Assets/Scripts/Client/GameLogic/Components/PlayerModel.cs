
using Demo.GameLogic.Entities;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.GameLogic.Componnets
{
    class PlayerModel : Model, IPropertyListener
    {
        Slider m_Hp = null;
        IEnumerator m_HpSliderUpdateCoroutine = null;
        readonly Vector3 kHpSliderPositionOffset = new Vector3(0, 2.5f, 0);

        public override Vector3 position
        {
            set
            {
                base.position = value;
                if (m_Hp != null)
                    m_Hp.transform.position = value + kHpSliderPositionOffset;
            }
        }

        public PlayerModel(Entity entity) : base(entity)
        {

        }

        public void OnPropertyValueChanged()
        {
            var property = entity.GetComponent<Property>();
            m_Hp.value = property.hp / property.maxHp;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            entity.GetComponent<Property>().onValueChanged += OnPropertyValueChanged;

            Game.Instance.resourceLoader.Load<GameObject>("Prefabs/hp", OnHpSliderLoadFinish);
        }

        protected override void OnDisable()
        {
            entity.GetComponent<Property>().onValueChanged -= OnPropertyValueChanged;
            GameObject.Destroy(m_Hp.gameObject);
            m_Hp = null;
            base.OnDisable();
        }

        protected void OnHpSliderLoadFinish(GameObject go)
        {
            m_Hp = GameObject.Instantiate(go, position, rotation).GetComponent<Slider>();
            m_Hp.transform.SetParent(GameObject.FindGameObjectWithTag("WorldHudCanvas").transform);
            m_Hp.transform.forward = Game.Instance.cameraManager.camera.transform.forward;
            m_Hp.transform.position = position + kHpSliderPositionOffset;
            OnPropertyValueChanged();
        }
    }
}

