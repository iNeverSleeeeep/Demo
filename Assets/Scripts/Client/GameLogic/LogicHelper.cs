using Demo.GameLogic.Abilities;
using Demo.GameLogic.Componnets;
using Demo.GameLogic.Entities;

namespace Demo.GameLogic
{
    static class LogicHelper 
    {
        public static float DoDamage(AbilityUnitDamageType type, float value, Entity target)
        {
            //Debug.Log("DoDamage type=" + type+" value="+value+" target="+target.ToString());
            if (type == AbilityUnitDamageType.Magical && target.property.isMagicImmune)
                value = 0;
            var old = target.property.hp;
            target.property.hp -= value;
            var change = old - target.property.hp;
            var listener = target.model as IPropertyListener;
            if (listener != null && change != 0)
                listener.OnPropertyValueChanged();
            return change;
        }

        public static float DoRecoverHp(float value, Entity target)
        {
            //Debug.Log("DoRecoverHp value=" + value + " target=" + target.ToString());
            var old = target.property.hp;
            target.property.hp += value;
            var change = old - target.property.hp;
            var listener = target.model as IPropertyListener;
            if (listener != null && change != 0)
                listener.OnPropertyValueChanged();
            return change;
        }

        public static float DoRecoverMana(float value, Entity target)
        {
            //Debug.Log("DoRecoverMana value=" + value + " target=" + target.ToString());
            var old = target.property.mana;
            target.property.mana += value;
            var change = old - target.property.mana;
            var listener = target.model as IPropertyListener;
            if (listener != null && change != 0)
                listener.OnPropertyValueChanged();
            return change;
        }
    }
}

