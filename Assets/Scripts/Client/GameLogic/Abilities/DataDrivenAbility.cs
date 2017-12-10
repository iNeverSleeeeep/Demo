using System.Collections.Generic;

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
        public EventCommand[] OnSpellStart;
        public EventCommand[] OnTime;
        public EventCommand[] OnToggleOn;
        public EventCommand[] OnToggleOff;
        public EventCommand[] OnChannelFinish;
        public EventCommand[] OnChannelInterrupted;
        public EventCommand[] OnChannelSucceeded;
        public EventCommand[] OnOwnerDied;
        public EventCommand[] OnOwnerSpawned;
        public EventCommand[] OnProjectileHitUnit;
        public EventCommand[] OnProjectileFinish;
        public EventCommand[] OnEquip;
        public EventCommand[] OnUnequip;
        public EventCommand[] OnUpgrade;
        public EventCommand[] OnAbilityPhaseStart;

        // Modifier
        public List<AbilityModifier> modifiers;
    }
}
