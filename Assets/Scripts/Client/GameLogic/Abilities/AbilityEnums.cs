
using System.ComponentModel;

namespace Demo.GameLogic.Abilities
{
    // 目标队伍
    public enum AbilityUnitTargetTeam
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        Both = 1 << 0,  //  全部

        /// <summary>
        /// 敌人
        /// </summary>
        [Description("敌人")]
        Enemy = 1 << 1,    //  敌人

        /// <summary>
        /// 友军
        /// </summary>
        [Description("友军")]
        Friendly = 1 << 2, //  友军

        /// <summary>
        /// ?
        /// </summary>
        [Description("Custom")]
        Custom = 1 << 3,   //	?
    }

    // 伤害类型
    public enum AbilityUnitDamageType
    {
        /// <summary>
        /// 魔法伤害
        /// </summary>
        [Description("魔法伤害")]
        Magical = 1 << 0,

        /// <summary>
        /// 物理伤害
        /// </summary>
        [Description("物理伤害")]
        Physical = 1 << 1,

        /// <summary>
        /// 纯粹伤害
        /// </summary>
        [Description("纯粹伤害")]
        Pure = 1 << 2,

        /// <summary>
        /// 已经移除  复合伤害
        /// </summary>
        [Description("复合伤害")]
        Composite = 1 << 3,

        /// <summary>
        /// 已经移除 生命移除
        /// </summary>
        [Description("生命移除")]
        HpRemove = 1 << 4,
    }

    // 技能类型
    public enum AbilityType
    {
        /// <summary>
        /// 普通技能
        /// </summary>
        [Description("普通技能")]
        Basic = 1,

        /// <summary>
        /// 终极技能
        /// </summary>
        [Description("终极技能")]
        Ultimate,

        /// <summary>
        /// 用于属性奖励
        /// </summary>
        [Description("用于属性奖励")]
        Attributes,

        /// <summary>
        /// Hidden
        /// </summary>
        [Description("Hidden")]
        Hidden,
    }

    // 触发条件（Event）
    public enum AbilityEventTrigger
    {
        /// <summary>
        /// 当施法动画（AbilityCastPoint）完成后
        /// </summary>
        [Description("OnSpellStart")]
        OnSpellStart = 1,

        /// <summary>
        /// 时间点
        /// </summary>
        [Description("OnSpellStart")]
        OnTime,

        /// <summary>
        /// 激活开关技能
        /// </summary>
        [Description("OnToggleOn")]
        OnToggleOn,

        /// <summary>
        /// 关闭开关技能
        /// </summary>
        [Description("OnToggleOff")]
        OnToggleOff,

        /// <summary>
        /// 在任何情况下中断持续施法
        /// </summary>
        [Description("OnChannelFinish")]
        OnChannelFinish,

        /// <summary>
        /// 提前结束持续施法
        /// </summary>
        [Description("OnChannelInterrupted")]
        OnChannelInterrupted,

        /// <summary>
        /// 经过持续施法时间（AbilityChannelTime），完整持续施法
        /// </summary>
        [Description("OnChannelSucceeded")]
        OnChannelSucceeded,

        /// <summary>
        /// 拥有该技能的单位死亡
        /// </summary>
        [Description("OnOwnerDied")]
        OnOwnerDied,

        /// <summary>
        /// 拥有该技能的单位出生
        /// </summary>
        [Description("OnOwnerSpawned")]
        OnOwnerSpawned,

        /// <summary>
        /// 当投射物与有效单位接触
        /// </summary>
        [Description("OnProjectileHitUnit")]
        OnProjectileHitUnit,

        /// <summary>
        /// 投射物完成其修正后距离
        /// </summary>
        [Description("OnProjectileFinish")]
        OnProjectileFinish,

        /// <summary>
        /// 具被捡起
        /// </summary>
        [Description("OnProjectileFinish")]
        OnEquip,

        /// <summary>
        /// 道具离开物品栏
        /// </summary>
        [Description("OnUnequip")]
        OnUnequip,

        /// <summary>
        /// 从用户界面升级此技能
        /// </summary>
        [Description("OnUpgrade")]
        OnUpgrade,

        /// <summary>
        /// 技能开始施法（单位转向目标前）
        /// </summary>
        [Description("OnAbilityPhaseStart")]
        OnAbilityPhaseStart,
    }

    // 触发条件（Modifier）
    public enum AbilityModifierTrigger
    {
        /// <summary>
        /// 定时
        /// </summary>
        [Description("定时")]
        OnIntervalThink = 1, // 定时
    }

    // 目标
    public enum AbilityTarget
    {
        /// <summary>
        /// 目标
        /// </summary>
        [Description("目标")]
        Target = 1,

        /// <summary>
        /// 释放者
        /// </summary>
        [Description("释放者")]
        Caster
    }
}


