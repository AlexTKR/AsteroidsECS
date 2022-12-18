using Leopotam.Ecs;
using Scripts.CommonBehaviours;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class RotationSystem : IEcsRunSystem
    {
        private EcsFilter<TransformComponent, RotationComponent, RotationSpeedComponent> _rotationFilter;
        
        public  void Run()
        {
            if (IPauseBehaviour.IsPaused)
                return;
            
            foreach (var i in _rotationFilter)
            {
                ref var rotationEntity = ref _rotationFilter.GetEntity(i);
                ref var transformComponent = ref _rotationFilter.Get1(i);
                ref var rotationComponent = ref _rotationFilter.Get2(i);
                ref var rotationSpeedComponent = ref _rotationFilter.Get3(i);

                var playerTransform = transformComponent.Transform;
                playerTransform.Rotate(playerTransform.forward * rotationComponent.Rotation * rotationSpeedComponent.RotationSpeed * Time.deltaTime);// Set op player 
                rotationEntity.Del<RotationComponent>();
            }
        }
    }
}
