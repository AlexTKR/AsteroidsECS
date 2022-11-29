using ECS.Systems;
using Scripts.ECS.Components;
using Scripts.ECS.Entity;
using Scripts.ECS.System;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class PlayerMovementSystem : SystemBase
    {
        private float _movementSpeed = 4f; //TODO REMOVE
        private float _rotationSpeed = 4f; //TODO REMOVE

        public override void Run()
        {
            var players = _world.GetEntity<PlayerComponent>();

            for (int i = 0; i < players.Length; i++)
            {
                var currPlayer = players[i];
                MovePlayer(currPlayer);
            }
        }

        private void MovePlayer(EntityBase entity)
        {
            var playerComponent = entity.GetComponent<PlayerComponent>();
            var playerMovementInputComponent = entity.GetComponent<PlayerMovementInputComponent>();
            var transformComponent = entity.GetComponent<TransformComponent>();
            if (playerMovementInputComponent is { })
            {
                playerComponent.Acceleration += playerMovementInputComponent.Acceleration;
                playerComponent.LastAccelerationDirection = transformComponent.Transform.up;
                entity.RemoveComponent<PlayerMovementInputComponent>();
            }

            if (playerComponent.Acceleration <= 0f)
                return;

            var playerTransform = transformComponent.Transform;
            playerTransform.localPosition += playerComponent.LastAccelerationDirection * playerComponent.Acceleration * Time.deltaTime;
            playerComponent.Acceleration = Mathf.Clamp(playerComponent.Acceleration - 0.01f, 0f, float.MaxValue); //TODO to game settings
        }
    }
}
