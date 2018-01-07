using Demo.GameLogic.Abilities;
using Demo.GameLogic.Componnets;
using Demo.GameLogic.Entities;
using UnityEngine;

namespace Demo.GameLogic
{
    static class LogicHelper 
    {
        public static float DoDamage(AbilityUnitDamageType type, float value, Entity target)
        {
            //Debug.Log("DoDamage type=" + type+" value="+value+" target="+target.ToString());
            var old = target.property.hp;
            target.property.hp -= value;
            var listener = target.model as IPropertyListener;
            if (listener != null)
                listener.OnPropertyValueChanged();
            return old - target.property.hp;
        }

        public static float DoRecoverHp(float value, Entity target)
        {
            //Debug.Log("DoRecoverHp value=" + value + " target=" + target.ToString());
            var old = target.property.hp;
            target.property.hp += value;
            var listener = target.model as IPropertyListener;
            if (listener != null)
                listener.OnPropertyValueChanged();
            return old - target.property.hp;
        }

        public static float DoRecoverMana(float value, Entity target)
        {
            //Debug.Log("DoRecoverMana value=" + value + " target=" + target.ToString());
            var old = target.property.mana;
            target.property.mana += value;
            var listener = target.model as IPropertyListener;
            if (listener != null)
                listener.OnPropertyValueChanged();
            return old - target.property.mana;
        }
    }
}

