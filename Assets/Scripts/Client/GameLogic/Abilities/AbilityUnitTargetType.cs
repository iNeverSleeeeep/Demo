using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.GameLogic.Abilities
{
    // 目标类型
    class AbilityUnitTargetType
    {
        /// <summary>
        /// 任意，包括隐藏的实体
        /// </summary>
        public bool All = false;

        /// <summary>
        /// 英雄
        /// </summary>
        public bool Hero = false;

        /// <summary>
        /// 基本单位, 包括召唤单位
        /// </summary>
        public bool Basic = false;

        /// <summary>
        /// 机械单位
        /// </summary>
        public bool Mechanical = false;

        /// <summary>
        /// 塔和建筑
        /// </summary>
        public bool Building = false;

        /// <summary>
        /// 树
        /// </summary>
        public bool Ttee = false;

        /// <summary>
        /// 与BASIC类似但是不包括一些召唤单位
        /// </summary>
        public bool Creep = false;

        /// <summary>
        /// 信使和飞行信使
        /// </summary>
        public bool Courier = false;

        /// <summary>
        /// 其他
        /// </summary>
        public bool Other = false;

        /// <summary>
        /// 未开放?
        /// </summary>
        public bool Custom = false;
    }
}
