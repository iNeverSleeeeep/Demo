
using System;
using UnityEngine;

namespace Demo.GameLogic.Abilities
{
    #region 辅助类 不是Event Element
    // 事件目标
    [Serializable]
    class EventTarget
    {
        public AbilityUnitTargetTeam team;
        public AbilityUnitTargetType type;
        public AbilityUnitTargetFlags flags;
        public float radius;
        public AbilityTarget center;
    }
    #endregion

    // 添加buff
    [Serializable]
    class ApplyModifier
    {
        public string modifierName;
        public EventTarget target;
    }

    // 发射子弹类
    [Serializable]
    class TrackingProjectile
    {
        public EventTarget target;
        public float visionRadius;
        public float moveSpeed;
    }

    // 伤害
    [Serializable]
    class Damage
    {
        public EventTarget target;
        public AbilityUnitDamageType type;
        public float value;
    }

    // 位移
    [Serializable]
    class ApplyMotionController
    {
        public float duration;
        public string script;
        public string horizontalControlFunction;
        public string verticalControlFunction;
        public EventTarget target;   
    }

    // 脚本
    [Serializable]
    class RunScript
    {
        public string script;
        public string function;
    }

}
