
using Demo.GameLogic.Componnets;
using Demo.GameLogic.Entities;
using UnityEngine;

namespace Demo.Level
{
    class WarlockWarsLevelLoader : LevelLoader
    {
        // TODO 20180106 现在是在entity的active里面初始化的model 所以顺序看上去有点奇怪 看看怎么修改一下
        public override void Load()
        {
            Game.Instance.gameLogicManager.abilitySystem.AddAbility("AbilityData/TestAttack");
            Game.Instance.gameLogicManager.abilitySystem.AddAbility("AbilityData/TestAttack2");

            // player
            var entity = Entity.Create(EntityType.Player);
            Game.Instance.gameLogicManager.entityManager.AddEntity(entity);
            Game.Instance.inputManager.entity = entity.id;
            entity.movement.defaultSpeed = 5;
            PlayerPropertyInitializer.Handle(entity.GetComponent<Property>());
            entity.active = true;
            entity.collider.size = 1;
            entity.camp.type = Camp.Type.Camp1;
            entity.collider.selfLayer = (int)GameLogic.Componnets.Collider.Layer.Hero;
            Game.Instance.cameraManager.target = entity.model.transform;
            entity.model.name = "player";
            entity.model.material.color = Color.blue;
            entity.ability.attack = "TestAttack2";
            entity.clickable.clickCallback = Game.Instance.inputManager.inputHandler.OnEntityClick;

            // computer
            for (var i = 0; i < 5; ++i)
            {
                var computer = Entity.Create(EntityType.Computer);
                Game.Instance.gameLogicManager.entityManager.AddEntity(computer);
                computer.GetComponent<Brain>().enemy = entity.id;
                computer.movement.defaultSpeed = 2;
                computer.camp.type = Camp.Type.Camp2;
                computer.collider.size = 1;
                computer.collider.selfLayer = (int)GameLogic.Componnets.Collider.Layer.Hero;
                computer.position.position = new Vector3(Utils.Random.Range(1, 20), 0, Utils.Random.Range(1, 20));
                PlayerPropertyInitializer.Handle(computer.GetComponent<Property>());
                computer.active = true;
                computer.model.name = "computer " + i.ToString();
                computer.model.position = computer.position.position;
                computer.clickable.clickCallback = Game.Instance.inputManager.inputHandler.OnEntityClick;
            }
        }
    }
}

