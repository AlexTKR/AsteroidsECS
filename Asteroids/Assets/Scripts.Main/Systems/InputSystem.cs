using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class InputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, MovableWithInertiaComponent> _playerFilter;
        private EcsWorld _ecsWorld;

        private PlayerInputActions _inputActions;
        private bool shootBulletsButtonPressed;
        private bool shootLaserButtonPressed;

        public void Init()
        {
            _inputActions = new PlayerInputActions();
            _inputActions.PlayerMap.Enable();
            _inputActions.PlayerMap.ShootingBullets.performed += context => { shootBulletsButtonPressed = true; };

            _inputActions.PlayerMap.ShootingLaser.performed += context => { shootLaserButtonPressed = true; };
        }

        public void Run()
        {
            var movementInput = _inputActions.PlayerMap.Movement.ReadValue<Vector2>();

            ProcessPlayerMovementInput(ref movementInput);


            if (shootBulletsButtonPressed)
            {
                shootBulletsButtonPressed = false;
                _ecsWorld.NewEntity().Get<ShootBulletComponent>();
            }
            
            // if (shootLaserButtonPressed)
            // {
            //     shootLaserButtonPressed = false;
            //     laserComponent = new ShootLaserComponent();
            // }

            // var laserEntities = _world.GetEntity<LaserComponent>();
            //
            // foreach (var i in _playerFilter)
            // {
            //     var playerEntity = _playerFilter.GetEntity(i);
            //
            //     if (playerMovementInputComponent)
            //         playerEntity.AddComponent(playerMovementInputComponent);
            //
            //     if (playerRotationInputComponent is { })
            //         playerEntity.AddComponent(playerRotationInputComponent);
            //
            //     if (bulletComponent is { })
            //         playerEntity.AddComponent(bulletComponent);
            // }

            // for (int i = 0; i < laserEntities.Length; i++)
            // {
            //     var laserEntity = laserEntities[i];
            //
            //     if (laserComponent is { } && laserEntity.GetComponent<DelayLaserComponent>() is null)
            //         laserEntity.AddComponent(laserComponent);
            // }
        }

        private void ProcessPlayerMovementInput(ref Vector2 movementInput)
        {
            foreach (var i in _playerFilter)
            {
                ref var playerEntity = ref _playerFilter.GetEntity(i);
                ref var movableWithInertiaComponent = ref _playerFilter.Get2(i);

                if (movementInput.y >= 1f)
                {
                    playerEntity.Get<AccelerationComponent>() = new AccelerationComponent()
                    {
                        Acceleration = movableWithInertiaComponent.InstantSpeed
                    };
                }

                if (movementInput.x != 0)
                {
                    playerEntity.Get<RotationComponent>() = new RotationComponent()
                    {
                        Rotation = movementInput.x
                    };
                }
            }
        }
    }
}