using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.GameLogic.Abilities
{
    // 目标标签
    class AbilityUnitTargetFlags
    {
        public bool Dead = false;

        // public bool MeleeOnly = false; // 拥有攻击类型DOTA_UNIT_CAP_MELEE_ATTACK的单位
        // public bool RangedOnly = false; // 拥有攻击类型DOTA_UNIT_CAP_RANGED_ATTACK的单位
        // public bool ManaOnly = false; // 在npc_unit中拥有魔法没有"StatusMana" "0"的单位
        // public bool CHeckDisableHel = false; // 禁用帮助的单位 尚不确定数据驱动技能如何使用？
        // public bool NoInvis = false; // 忽略拥有MODIFIER_STATE_INVISIBLE的不可见单位
        // public bool MagicImmuneEnemies = false; // 指向拥有MODIFIER_STATE_MAGIC_IMMUNE （魔法免疫）的敌人单位 例子: 根须缠绕, 淘汰之刃, 原始咆哮...
        // public bool NotMagicImmuneAllies = false; // 忽略拥有MODIFIER_STATE_MAGIC_IMMUNE（魔法免疫）的友军 例子: 痛苦之源噩梦
        // public bool NotAttackImmune = false; // 拥有MODIFIER_STATE_ATTACK_IMMUNE的单位（攻击免疫单位）
        // public bool FowVisible = false; // 单位离开视野时打断 例子: 魔法汲取, 生命汲取
        // public bool Invulnerable = false; // 拥有MODIFIER_STATE_INVULNERABLE的单位（无敌单位） 例子: 暗杀, 召回, 巨力重击...
        // public bool NotAncients = false; // 忽略单位，使用标签"IsAncient" "1" 例子: 麦达斯之手
        // public bool NotCreepHero = false; // 忽略单位，使用标签"ConsideredHero" "1" 例子: 星体禁锢, 崩裂禁锢, 灵魂隔断
        // public bool NotDominated = false; //拥有MODIFIER_STATE_DOMINATED的单位（被支配的单位）
        // public bool NotIllusions = false; // 拥有MODIFIER_PROPERTY_IS_ILLUSION的单位（幻象单位）
        // public bool NotNightMared = false; // 拥有MODIFIER_STATE_NIGHTMARED的单位（噩梦中单位）
        // public bool NotSummoned = false; // 通过SpawnUnit Action创建的单位
        // public bool OutOfWorld = false; // 拥有MODIFIER_STATE_OUT_OF_GAME的单位（离开游戏的单位）
        // public bool PlayerControlled = false; // 玩家控制的单位，接近Lua IsControllableByAnyPlayer()
    }
}
