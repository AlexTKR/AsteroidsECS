using Leopotam.Ecs;
using Scripts.Main.Components;
using Scripts.Main.Converters;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        private EcsFilter<MovableComponent, TransformComponent, GameObjectComponent> _movableFilter;

        public void Run()
        {
            foreach (var i in _movableFilter)
            {
                ref var entity = ref _movableFilter.GetEntity(i);
                ref var movableComponent = ref _movableFilter.Get1(i);
                ref var transformComponent = ref _movableFilter.Get2(i);
                ref var gameObjectComponent = ref _movableFilter.Get3(i);
                
                if (!gameObjectComponent.GameObject.activeSelf)
                    continue;

                Vector3 direction = default;
                if (entity.Has<FollowTargetComponent>())
                {
                    ref var followTargetComponent = ref entity.Get<FollowTargetComponent>();
                    direction = followTargetComponent.Target.position - transformComponent.Transform.position;
                }
                else
                {
                    direction = movableComponent.Direction;
                }
                
                transformComponent.Transform.localPosition += direction * movableComponent.Speed * Time.deltaTime;
            }
        }
    }
}