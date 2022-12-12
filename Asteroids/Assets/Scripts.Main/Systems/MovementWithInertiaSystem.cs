using Leopotam.Ecs;
using Scripts.Main.Components;
using Scripts.Main.Settings;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class MovementWithInertiaSystem : IEcsRunSystem
    {
        private EcsFilter<TransformComponent, MovableWithInertiaComponent> _movableFilter;

        public void Run()
        {
            foreach (var i in _movableFilter)
            {
                ref var movableEntity = ref _movableFilter.GetEntity(i);
                ref var movableWithInertiaComponent = ref _movableFilter.Get2(i);
                ref var transformComponent = ref _movableFilter.Get1(i);

                if (movableEntity.Has<AccelerationComponent>())
                {
                    ref var accelerationComponent = ref movableEntity.Get<AccelerationComponent>();
                    movableWithInertiaComponent.CurrentSpeed += accelerationComponent.Acceleration;
                    movableWithInertiaComponent.LastAccelerationDirection = transformComponent.Transform.up;
                    movableEntity.Del<AccelerationComponent>();
                }

                if (movableWithInertiaComponent.CurrentSpeed <= 0)
                    continue;
                
                transformComponent.Transform.localPosition += movableWithInertiaComponent.LastAccelerationDirection *
                                                              movableWithInertiaComponent.CurrentSpeed * Time.deltaTime;
                movableWithInertiaComponent.CurrentSpeed = Mathf.Clamp(
                    movableWithInertiaComponent.CurrentSpeed - movableWithInertiaComponent.SpeedDecreaseValue, 0f,
                    float.MaxValue);
            }
        }
    }
}