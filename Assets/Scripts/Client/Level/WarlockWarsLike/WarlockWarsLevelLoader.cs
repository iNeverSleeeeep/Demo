
using Demo.GameLogic.Componnets;
using Demo.GameLogic.Entities;
using UnityEngine;

namespace Demo.Level
{
    class WarlockWarsLevelLoader : LevelLoader
    {
        public override void Load()
        {
            Game.Instance.gameLogicManager.abilitySystem.AddAbility("AbilityData/TestAbility");

            // player
            var entity = EntityFactory.CreateEntity(EntityFactory.EntityType.Player);
            Game.Instance.gameLogicManager.entityManager.AddEntity(entity);
            Game.Instance.inputManager.entity = entity.id;
            entity.movement.defaultSpeed = 5;
            PlayerPropertyInitializer.Handle(entity.GetComponent<Property>());
            foreach (var component in entity.GetAllComponents())
                component.enabled = true;
            Game.Instance.cameraManager.target = entity.model.transform;
            entity.model.name = "player";
            entity.model.material.color = Color.blue;
            entity.ability.attack = "TestAbility";
            entity.clickable.clickCallback = Game.Instance.inputManager.inputHandler.OnEntityClick;

            // computer
            for (var i = 0; i < 5; ++i)
            {
                var computer = EntityFactory.CreateEntity(EntityFactory.EntityType.Computer);
                Game.Instance.gameLogicManager.entityManager.AddEntity(computer);
                computer.GetComponent<Brain>().enemy = entity.id;
                computer.movement.defaultSpeed = 2;
                computer.position.position = new Vector3(Utils.Random.Range(1, 20), 0, Utils.Random.Range(1, 20));
                PlayerPropertyInitializer.Handle(computer.GetComponent<Property>());
                foreach (var component in computer.GetAllComponents())
                    component.enabled = true;
                computer.model.name = "computer " + i.ToString();
                computer.model.position = computer.position.position;
                computer.clickable.clickCallback = Game.Instance.inputManager.inputHandler.OnEntityClick;
            }
        }
    }
}

