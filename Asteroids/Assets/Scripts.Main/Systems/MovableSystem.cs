using ECS.Systems;
using Scripts.ECS.Components;
using Scripts.ECS.System;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class MovableSystem : SystemBase
    {
        public override void Run()
        {
            base.Run();

            var movableEntities = _world.GetEntity<MovableComponent>();

            for (int i = 0; i < movableEntities.Length; i++)
            {
                var current = movableEntities[i];
                var movableComponent = current.GetComponent<MovableComponent>();
                var transformComponent = current.GetComponent<TransformComponent>();
                if (transformComponent is null)
                    continue;

                var followTargetComponent = current.GetComponent<FollowTargetComponent>();
                var direction = followTargetComponent is { }
                    ? (followTargetComponent.Target.position - transformComponent.Transform.position).normalized
                    : movableComponent.Direction;

                transformComponent.Transform.localPosition += direction * movableComponent.Speed * Time.deltaTime;
            }
        }
    }
}