using Leopotam.Ecs;
using Scripts.CommonBehaviours;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class InputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, MovableWithInertiaComponent> _playerFilter;
        private EcsFilter<LaserComponent> _laserFilter;
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
            if (IPauseBehaviour.IsPaused)
                return;
            
            var movementInput = _inputActions.PlayerMap.Movement.ReadValue<Vector2>();

            ProcessPlayerMovementInput(ref movementInput);


            if (shootBulletsButtonPressed)
            {
                shootBulletsButtonPressed = false;
                _ecsWorld.NewEntity().Get<ShootBulletComponent>();
                return;
            }

            if (shootLaserButtonPressed)
            {
                shootLaserButtonPressed = false;
                ref var laserEntity = ref _laserFilter.GetEntity(0);
                laserEntity.Get<ShootLaserComponent>();
            }
        }

        private void ProcessPlayerMovementInput(ref Vector2 movementInput)
        {
            if(_playerFilter.IsEmpty())
                return;
            
            ref var playerEntity = ref _playerFilter.GetEntity(0);
            ref var movableWithInertiaComponent = ref _playerFilter.Get2(0);

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