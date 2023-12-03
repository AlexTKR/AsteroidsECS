using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class MovementSystem : PausableSystem
    {
        private EcsFilter<MovableComponent, TransformComponent, GameObjectComponent> _movableFilter;

        protected override void Tick()
        {
            foreach (var i in _movableFilter)
            {
                ref var entity = ref _movableFilter.GetEntity(i);
                ref MovableComponent movableComponent = ref _movableFilter.Get1(i);
                ref TransformComponent transformComponent = ref _movableFilter.Get2(i);
                ref GameObjectComponent gameObjectComponent = ref _movableFilter.Get3(i);

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