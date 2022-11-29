using ECS.Systems;
using Scripts.ECS.Components;
using Scripts.ECS.Entity;
using Scripts.ECS.System;
using Scripts.Main.Components;
using Scripts.Main.Settings;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class PlayerRotationSystem : SystemBase
    {
        public override void Run()
        {
            var players = _world.GetEntity<PlayerComponent>();

            for (int i = 0; i < players.Length; i++)
            {
                var currPlayer = players[i];
                RotatePlayer(currPlayer);
            }
        }
        
        private void RotatePlayer(EntityBase entity)
        {
            var playerRotationInputComponent = entity.GetComponent<PlayerRotationInputComponent>();
            if (playerRotationInputComponent is null)
                return;

            var transform = entity.GetComponent<TransformComponent>();
            var playerTransform = transform.Transform;
            playerTransform.Rotate(playerTransform.forward * playerRotationInputComponent.Rotation * RuntimeSharedData.GameSettings.playerRotationSpeed * Time.deltaTime); 
            entity.RemoveComponent<PlayerRotationInputComponent>();
        }
    }
}
