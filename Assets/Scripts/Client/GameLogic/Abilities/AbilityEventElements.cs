
using UnityEngine;

namespace Demo.GameLogic.Abilities
{
    #region 辅助类 不是Event Element
    // 事件目标
    class EventTarget : ScriptableObject
    {
        public AbilityUnitTargetTeam team;
        public AbilityUnitTargetType type;
        public AbilityUnitTargetFlags flags;
        public float radius;
        public AbilityTarget center;
    }
    #endregion

    // 添加buff
    class ApplyModifier : ScriptableObject
    {
        public string modifierName;
        public AbilityTarget target;
    }

    // 发射子弹类
    class TrackingProjectile : ScriptableObject
    {
        public AbilityTarget target;
        public float visionRadius;
        public float moveSpeed;
    }

    // 伤害
    class Damage : ScriptableObject
    {
        public EventTarget target;
        public AbilityUnitDamageType type;
    }

    // 位移
    class ApplyMotionController : ScriptableObject
    {
        public float duration;
        public string script;
        public string horizontalControlFunction;
        public string verticalControlFunction;
        public EventTarget target;   
    }

    // 脚本
    class RunScript : ScriptableObject
    {
        public string script;
        public string function;
    }

}
