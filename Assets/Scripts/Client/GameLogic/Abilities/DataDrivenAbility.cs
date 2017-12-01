
using System.Collections.Generic;
using UnityEngine;

namespace Demo.GameLogic.Abilities
{
    class DataDrivenAbility
    {
        public string name;

        public int abilityLevel;
        // 技能类型
        public AbilityBehaviour behaviour;
        // 目标队伍
        public AbilityUnitTargetTeam targetTeam;
        // 目标类型
        public AbilityUnitTargetType targetType;
        // 目标标签
        public AbilityUnitTargetFlags targetFlags;
        // 前摇
        public float castPoint;
        // 释放距离
        public float castRange;
        // 冷却时间
        public float cooldown;
        // 魔法消耗
        public float manaCost;

        // 技能事件
        public List<AbilityEvent> events;

        // Modifier
        public List<AbilityModifier> modifiers;
    }
}
