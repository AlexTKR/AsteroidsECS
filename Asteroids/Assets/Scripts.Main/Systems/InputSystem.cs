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
            ShootBulletComponent bulletComponent = null;
            ShootLaserComponent laserComponent = null;

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
                bulletComponent = new ShootBulletComponent();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                laserComponent = new ShootLaserComponent();
            }

            var playerEntities = _world.GetEntity<PlayerComponent>();
            var laserEntities = _world.GetEntity<LaserComponent>();

            for (int i = 0; i < playerEntities.Length; i++)
            {
                var playerEntity = playerEntities[i];

                if (playerMovementInputComponent is { })
                    playerEntity.AddComponent(playerMovementInputComponent);

                if (playerRotationInputComponent is { })
                    playerEntity.AddComponent(playerRotationInputComponent);

                if (bulletComponent is { })
                    playerEntity.AddComponent(bulletComponent);
            }


            for (int i = 0; i < laserEntities.Length; i++)
            {
                var laserEntity = laserEntities[i];

                if (laserComponent is { } && laserEntity.GetComponent<DelayLaserComponent>() is null)
                    laserEntity.AddComponent(laserComponent);
            }
        }
    }
}