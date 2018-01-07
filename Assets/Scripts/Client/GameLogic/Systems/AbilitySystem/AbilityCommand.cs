using Demo.GameLogic.Abilities;
using Demo.GameLogic.Componnets;
using Demo.GameLogic.Entities;
using Demo.Utils;
using System.Collections.Generic;

namespace Demo.GameLogic.Systems
{
    struct AbilityCommandContext
    {
        public int caster;
        public int target;
        
        public Dictionary<string, ModifierRoot> modifiers;
        public Dictionary<AbilityEventTrigger, AbilityContext.TriggerEvent> triggers;
    }

    interface IAbilityCommand
    {
        void Execute(AbilityCommandContext ctx);
    }

    class ApplyModifierCommand : IAbilityCommand
    {
        private string m_ModifierName;
        public ApplyModifierCommand(ApplyModifier modifier)
        {
            m_ModifierName = modifier.modifierName;
        }

        public void Execute(AbilityCommandContext ctx)
        {
            var entityManager = Game.Instance.gameLogicManager.entityManager;
            var target = entityManager.GetEntity(ctx.target);
            if (target != null)
            {
                ModifierRoot modifier;
                if (ctx.modifiers.TryGetValue(m_ModifierName, out modifier))
                {
                    modifier = modifier.Clone() as ModifierRoot;
                    modifier.context = new ModifierContext()
                    {
                        caster = ctx.caster,
                        owner = target.id,
                    };

                    target.modifier.AddModifier(modifier);
                }
            }
        }
    }

    class DamageCommand : IAbilityCommand
    {
        private AbilityUnitDamageType m_DamageType;
        private float m_DamageValue;

        public DamageCommand(Damage damage)
        {
            m_DamageType = damage.type;
            m_DamageValue = damage.value;
        }

        public void Execute(AbilityCommandContext ctx)
        {
            var entityManager = Game.Instance.gameLogicManager.entityManager;
            var target = entityManager.GetEntity(ctx.target);
            if (target != null)
            {
                LogicHelper.DoDamage(m_DamageType, m_DamageValue, target);
            }
        }
    }

    class TrackingProjectileCommand : IAbilityCommand
    {
        private float m_VisionRadius;
        private float m_MoveSpeed;

        public TrackingProjectileCommand(TrackingProjectile tp)
        {
            m_MoveSpeed = tp.moveSpeed;
            m_VisionRadius = tp.visionRadius;
        }

        public void Execute(AbilityCommandContext ctx)
        {
            var entityManager = Game.Instance.gameLogicManager.entityManager;
            var caster = entityManager.GetEntity(ctx.caster);
            var target = entityManager.GetEntity(ctx.target);

            var tp = Entity.Create(EntityType.TrackingProjectile);
            entityManager.AddEntity(tp);
            tp.collider.size = m_VisionRadius;
            tp.collider.selfLayer = (int)Collider.Layer.Projectile;
            tp.collider.maskLayer = (int)Collider.Layer.Hero;
            tp.movement.defaultSpeed = tp.movement.speed = m_MoveSpeed;
            tp.tracker.target = target.id;
            tp.position.position = caster.position.position;
            tp.active = true;
            tp.model.name = "tp caster:" + caster.ToString() + " target:" + target.ToString();
            tp.model.position = caster.position.position;
            tp.model.material.color = UnityEngine.Color.red;
            tp.camp.type = caster.camp.type;

            tp.collider.onCollide = (projectile, other) =>
            {
                if (ctx.target != other.id)
                    return;
                if (ctx.triggers.ContainsKey(AbilityEventTrigger.OnProjectileHitUnit))
                    ctx.triggers[AbilityEventTrigger.OnProjectileHitUnit].Invoke(other.id);

                Entity.Destroy(projectile);
            };
        }
    }

    class AttackSpeedCommand : IAbilityCommand
    {
        private int m_AttackSpeedValue;

        public AttackSpeedCommand(AttackSpeed attackSpeed)
        {
            m_AttackSpeedValue = attackSpeed.value;
        }

        public void Execute(AbilityCommandContext ctx)
        {
            var entityManager = Game.Instance.gameLogicManager.entityManager;
            var target = entityManager.GetEntity(ctx.target);
            if (target != null)
            {
                // TODO 20180107 攻速 property
            }
        }
    }

    class MagicImmuneCommand : IAbilityCommand
    {
        private int m_MagicImmuneValue;

        public MagicImmuneCommand(MagicImmune magicImmune)
        {
            m_MagicImmuneValue = magicImmune.value;
        }

        public void Execute(AbilityCommandContext ctx)
        {
            var entityManager = Game.Instance.gameLogicManager.entityManager;
            var target = entityManager.GetEntity(ctx.target);
            if (target != null)
            {
                target.property.magicImmuneBuffCount += m_MagicImmuneValue;
            }
        }
    }
}


