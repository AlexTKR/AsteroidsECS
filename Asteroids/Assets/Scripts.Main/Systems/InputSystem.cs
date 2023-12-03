using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class InputSystem : PausableSystem, IEcsInitSystem
    {
        private EcsFilter<PlayerComponent, MovableWithInertiaComponent> _playerFilter;
        private EcsFilter<LaserComponent> _laserFilter;
        private EcsWorld _ecsWorld;

        private PlayerInputActions _inputActions;
        private bool _shootBulletsButtonPressed;
        private bool _shootLaserButtonPressed;

        public void Init()
        {
            _inputActions = new PlayerInputActions();
            _inputActions.PlayerMap.Enable();
            _inputActions.PlayerMap.ShootingBullets.performed += context => { _shootBulletsButtonPressed = true; };

            _inputActions.PlayerMap.ShootingLaser.performed += context => { _shootLaserButtonPressed = true; };
        }

        protected override void Tick()
        {
            var movementInput = _inputActions.PlayerMap.Movement.ReadValue<Vector2>();
            ProcessPlayerMovementInput(ref movementInput);


            if (_shootBulletsButtonPressed)
            {
                _shootBulletsButtonPressed = false;
                _ecsWorld.NewEntity().Get<ShootBulletComponent>();
                return;
            }

            if (_shootLaserButtonPressed)
            {
                _shootLaserButtonPressed = false;
                ref var laserEntity = ref _laserFilter.GetEntity(0);
                laserEntity.Get<ShootLaserComponent>();
            }
        }

        private void ProcessPlayerMovementInput(ref Vector2 movementInput)
        {
            if(_playerFilter.IsEmpty())
                return;
            
            ref var playerEntity = ref _playerFilter.GetEntity(0);
            ref MovableWithInertiaComponent movableWithInertiaComponent = ref _playerFilter.Get2(0);

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