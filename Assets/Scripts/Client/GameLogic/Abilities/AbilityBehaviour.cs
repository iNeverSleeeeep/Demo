using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.GameLogic.Abilities
{
    // 技能类型
    class AbilityBehaviour
    {
        /// <summary>
        /// 无目标技能，不需要选择目标释放，按下技能按钮即可释放
        /// </summary>
        public bool NoTarget = false;

        /// <summary>
        /// 单位目标技能，需要目标释放，需要AbilityUnitTargetTeam和AbilityUnitTargetType，参见目标
        /// </summary>
        public bool UnitTarget = false;

        /// <summary>
        /// 位置目标技能，可以对鼠标指向的任意位置释放，如果点击单位也只是对其位置释放
        /// </summary>
        public bool Point = false;

        /// <summary>
        /// 被动技能，不能也不需要释放
        /// </summary>
        public bool Passive = false;

        /// <summary>
        /// 持续施法技能，施法者移动或者被沉默、眩晕就会打断
        /// </summary>
        public bool Channelled = false;

        /// <summary>
        /// 开关技能，可以开关
        /// </summary>
        public bool Toggle = false;

        /// <summary>
        /// 光环技能，并没有实际作用而只是作为一个标签
        /// </summary>
        public bool Aura = false;

        /// <summary>
        /// 自动施法，可以自动施法，通常如果不是一个 ATTACK技能的话本身并不工作
        /// </summary>
        public bool AutoCast = false;

        /// <summary>
        /// 隐藏，不能释放并且不在HUD上显示
        /// </summary>
        public bool Hidden = false;

        /// <summary>
        /// AOE技能，会显示技能将要影响的范围，类似点目标技能，需要配合AoERadius来使用
        /// </summary>
        public bool Aoe = false;

        /// <summary>
        /// 不可学习技能，不能通过点击HUD学习，例如卡尔的技能
        /// </summary>
        public bool NotLearnable = false;

        /// <summary>
        /// 道具技能，技能与道具绑定，并不需要使用，游戏会在自动将此属性添加给任何基类为"item_datadriven"的技能
        /// </summary>
        public bool Item = false;

        /// <summary>
        /// 方向技能，拥有一个从英雄出发的方向，例如白虎的箭和屠夫的钩子
        /// </summary>
        public bool Directional = false;

        /// <summary>
        /// 立即释放技能，立即释放而不进入操作序列
        /// </summary>
        public bool Immediate = false;

        /// <summary>
        /// 技能没有辅助刻度 ？
        /// </summary>
        public bool NoAssist = false;

        /// <summary>
        /// 是 ATTACK（攻击）技能，不能攻击不能被攻击的单位
        /// </summary>
        public bool Attack = false;

        /// <summary>
        /// 被定身时无法使用
        /// </summary>
        public bool RootDisables = false;

        /// <summary>
        /// 指令被限制时依然能够使用，例如食尸鬼大招
        /// </summary>
        public bool Unrestricted = false;

        /// <summary>
        /// 释放在敌人身上时不会警告他们，例如白牛的冲
        /// </summary>
        public bool DontAlertTarget = false;

        /// <summary>
        /// 完成施法后不会恢复移动，只能对无目标、非立即技能生效
        /// </summary>
        public bool DontResumeMovement = false;

        /// <summary>
        /// 完成施法后不会继续继续攻击之前的目标，只对无目标，非立即释放和单位目标技能生效
        /// </summary>
        public bool DontResumeAttack = false;

        /// <summary>
        /// 被偷窃时依然使用其默认施法前摇，例如地卜师的忽悠和先知的传送
        /// </summary>
        public bool NormalWhenStolen = false;

        /// <summary>
        /// 无视施法后摇
        /// </summary>
        public bool IgnoreBackswing = false;

        /// <summary>
        /// 在被眩晕、施法时或者强制攻击时都能执行，只对开关技能有效，例如变形
        /// </summary>
        public bool IgnorePseudoQueue = false;

        /// <summary>
        /// 可对符文释放
        /// </summary>
        public bool RuneTarget = false;

        /// <summary>
        /// 不打断自己的持续施法
        /// </summary>
        public bool DontCancelChannel = false;

        /// <summary>
        /// OptionalUnitTarget
        /// </summary>
        public bool OptionalUnitTarget = false;
    }
}
