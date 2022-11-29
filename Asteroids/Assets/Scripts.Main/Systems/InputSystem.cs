using ECS.Systems;
using Scripts.ECS.System;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class InputSystem : SystemBase
    {
        public override void Run()
        {
            PlayerMovementInputComponent playerMovementInputComponent = null;
            PlayerRotationInputComponent playerRotationInputComponent = null;
            ShootComponent shootComponent = null;

            if (Input.GetKey(KeyCode.W))
            {
                (playerMovementInputComponent = new PlayerMovementInputComponent()).Acceleration += 0.03f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                (playerRotationInputComponent ??= new PlayerRotationInputComponent()).Rotation = -1f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                (playerRotationInputComponent ??= new PlayerRotationInputComponent()).Rotation = 1f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                shootComponent = new ShootComponent() { ShootType = ShootType.Bullet };
            }
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                shootComponent = new ShootComponent() { ShootType = ShootType.Laser };
            }

            var playerEntities = _world.GetEntity<PlayerComponent>();

            for (int i = 0; i < playerEntities.Length; i++)
            {
                var playerEntity = playerEntities[i];
                
                if(playerMovementInputComponent is {}) 
                    playerEntity.AddComponent(playerMovementInputComponent);
                
                if(playerRotationInputComponent is {})
                    playerEntity.AddComponent(playerRotationInputComponent);

                if (shootComponent is { })
                    playerEntity.AddComponent(shootComponent);
            }
        }
    }
}