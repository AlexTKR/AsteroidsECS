using Scripts.CommonExtensions;
using Scripts.ECS.Components;
using Scripts.ECS.Entity;
using Scripts.ECS.System;
using Scripts.ECS.World;
using Scripts.Main.Components;
using Scripts.Main.Controllers;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class ScreenBoundariesSystem : SystemBase
    {
        private Vector2 _screenBounds;

        public override void Init(WorldBase world)
        {
            base.Init(world);
            _screenBounds = _world.GetBehavior<IGetScreenBounds>().ScreenBounds;
        }

        public override void Run()
        {
            var affectedByBoundaries = _world.GetEntity<AffectedByBoundariesComponent>();

            for (int i = 0; i < affectedByBoundaries.Length; i++)
            {
                var currEntity = affectedByBoundaries[i];
                var transformComponent = currEntity.GetComponent<TransformComponent>();

                if (transformComponent is null)
                    continue;

                var playerComponent = currEntity.GetComponent<PlayerComponent>();

                if (playerComponent is { })
                {
                    HandlePlayer(currEntity, transformComponent);
                    continue;
                }

                HandleObjects(currEntity, transformComponent);
            }
        }

        private void HandlePlayer(EntityBase playerEntity, TransformComponent transformComponent)
        {
            GetBoundingData(playerEntity, transformComponent, out var objectHalfSize, out var absPosition,
                out var position);

            transformComponent.Transform.position = new Vector3(
                absPosition.x >= _screenBounds.x + objectHalfSize.x ? -position.x : position.x,
                absPosition.y >= _screenBounds.y + objectHalfSize.y ? -position.y : position.y, position.z);
        }

        private void HandleObjects(EntityBase entity, TransformComponent transformComponent)
        {
            GetBoundingData(entity, transformComponent, out var objectHalfSize, out var absPosition,
                out var position);

            var affectedByBoundariesComponent = entity.GetComponent<AffectedByBoundariesComponent>();

            if (absPosition.x >= _screenBounds.x + objectHalfSize.x + affectedByBoundariesComponent.BoundsOffset.x ||
                absPosition.y >= _screenBounds.y + objectHalfSize.y + affectedByBoundariesComponent.BoundsOffset.y)
            {
                var gameObjectComponent = entity.GetComponent<GameObjectComponent>();
                if (gameObjectComponent is { })
                    gameObjectComponent.GameObject.SetActiveOptimized(false);

                entity.AddComponent(new RecyclingComponent());
            }
        }

        private void GetBoundingData(EntityBase entity, TransformComponent transformComponent,
            out Vector2 objectHalfSize, out Vector2 absPosition, out Vector3 position)
        {
            var spriteRendererComponent = entity.GetComponent<SpriteRendererComponent>();
            var boundsSize = spriteRendererComponent.SpriteRenderer.bounds.size;
            objectHalfSize = new Vector2(boundsSize.x / 2, boundsSize.y / 2);
            position = transformComponent.Transform.position;
            absPosition = new Vector2(Mathf.Abs(position.x), Mathf.Abs(position.y));
        }
    }
}