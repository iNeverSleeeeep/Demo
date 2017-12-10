using Demo.GameLogic.Abilities;
using Demo.GameLogic.Componnets;
using Demo.GameLogic.Entities;
using UnityEngine;

namespace Demo.GameLogic.Systems
{
    static class AbilityHelper 
    {
        public static float DoDamage(AbilityUnitDamageType type, float value, Entity target)
        {
            Debug.Log("DoDamage type=" + type+" value="+value+" target="+target);
            var old = target.property.hp;
            target.property.hp -= value;
            var listener = target.model as IPropertyListener;
            if (listener != null)
                listener.OnPropertyValueChanged();
            return old - target.property.hp;
        }
    }
}

