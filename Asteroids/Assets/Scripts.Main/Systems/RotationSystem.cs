using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class RotationSystem : PausableSystem
    {
        private EcsFilter<TransformComponent, RotationComponent, RotationSpeedComponent> _rotationFilter;
        
        protected override void Tick()
        {
            foreach (var i in _rotationFilter)
            {
                ref var rotationEntity = ref _rotationFilter.GetEntity(i);
                ref TransformComponent transformComponent = ref _rotationFilter.Get1(i);
                ref RotationComponent rotationComponent = ref _rotationFilter.Get2(i);
                ref RotationSpeedComponent rotationSpeedComponent = ref _rotationFilter.Get3(i);

                var playerTransform = transformComponent.Transform;
                playerTransform.Rotate(playerTransform.forward * rotationComponent.Rotation * rotationSpeedComponent.RotationSpeed * Time.deltaTime);// Set op player 
                rotationEntity.Del<RotationComponent>();
            }
        }
    }
}
