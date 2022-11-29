using ECS.Systems;
using Scripts.ECS.System;
using Scripts.ECS.World;
using Scripts.Main.Components;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace Scripts.Main.Systems
{
    public class InputSystem : SystemBase
    {
        private PlayerInputActions _inputActions;
        private bool shootBulletsButtonPressed;
        private bool shootLaserButtonPressed;

        public override void Init(WorldBase world)
        {
            base.Init(world);
            _inputActions = new PlayerInputActions();
            _inputActions.PlayerMap.Enable();
            _inputActions.PlayerMap.ShootingBullets.performed += context => { shootBulletsButtonPressed = true; };

            _inputActions.PlayerMap.ShootingLaser.performed += context => { shootLaserButtonPressed = true; };
        }

        public override void Run()
        {
            var movementInput = _inputActions.PlayerMap.Movement.ReadValue<Vector2>();

            PlayerMovementInputComponent playerMovementInputComponent = null;
            PlayerRotationInputComponent playerRotationInputComponent = null;
            ShootBulletComponent bulletComponent = null;
            ShootLaserComponent laserComponent = null;

            if (movementInput.y >= 1f)
            {
                (playerMovementInputComponent = new PlayerMovementInputComponent()).Acceleration += 0.03f;
            }

            if (movementInput.x != 0)
            {
                (playerRotationInputComponent ??= new PlayerRotationInputComponent()).Rotation = movementInput.x;
            }

            if (shootBulletsButtonPressed)
            {
                shootBulletsButtonPressed = false;
                bulletComponent = new ShootBulletComponent();
            }

            if (shootLaserButtonPressed)
            {
                shootLaserButtonPressed = false;
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