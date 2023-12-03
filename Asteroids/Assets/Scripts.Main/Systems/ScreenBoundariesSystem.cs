using Leopotam.Ecs;
using Scripts.CommonExtensions;
using Scripts.Main.Components;
using Scripts.Main.Controllers;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class ScreenBoundariesSystem : PausableSystem
    {
        private IScreenBoundsProvider _screenBoundsProvider;

        private EcsFilter<AffectedByBoundariesComponent, TransformComponent, SpriteRendererComponent,
            GameObjectComponent> _boundariesFilter;

        protected override void Tick()
        {
            ref var screenBounds = ref _screenBoundsProvider.ScreenBounds;

            foreach (var i in _boundariesFilter)
            {
                ref var currEntity = ref _boundariesFilter.GetEntity(i);
                ref TransformComponent transformComponent = ref _boundariesFilter.Get2(i);
                ref SpriteRendererComponent spriteRendererComponent = ref _boundariesFilter.Get3(i);
                ref GameObjectComponent gameObjectComponent = ref _boundariesFilter.Get4(i);
                
                if (!gameObjectComponent.GameObject.activeSelf)
                    continue;

                if (currEntity.Has<PlayerComponent>())
                {
                    HandlePlayer(ref transformComponent, ref spriteRendererComponent,
                        ref screenBounds);
                    continue;
                }

                HandleObjects(ref currEntity, ref transformComponent, ref spriteRendererComponent,
                    ref screenBounds);
            }
        }

        private void HandlePlayer(ref TransformComponent transformComponent,
            ref SpriteRendererComponent spriteRendererComponent, ref Vector2 screenBounds)
        {
            GetBoundingData(ref spriteRendererComponent, ref transformComponent, out Vector2 objectHalfSize,
                out Vector2 absPosition,
                out Vector3 position);

            transformComponent.Transform.position = new Vector3(
                absPosition.x >= screenBounds.x + objectHalfSize.x ? -position.x : position.x,
                absPosition.y >= screenBounds.y + objectHalfSize.y ? -position.y : position.y, position.z);
        }

        private void HandleObjects(ref EcsEntity entity, ref TransformComponent transformComponent,
            ref SpriteRendererComponent spriteRendererComponent, ref Vector2 screenBounds)
        {
            GetBoundingData(ref spriteRendererComponent, ref transformComponent, out Vector2 objectHalfSize,
                out Vector2 absPosition,
                out Vector3 position);

            ref var affectedByBoundariesComponent = ref entity.Get<AffectedByBoundariesComponent>();

            if (absPosition.x >= screenBounds.x + objectHalfSize.x + affectedByBoundariesComponent.BoundsOffset.x ||
                absPosition.y >= screenBounds.y + objectHalfSize.y + affectedByBoundariesComponent.BoundsOffset.y)
            {
                if (entity.Has<GameObjectComponent>())
                {
                    var gameObjectComponent = entity.Get<GameObjectComponent>();
                    gameObjectComponent.GameObject.SetActiveOptimized(false);
                }

                entity.Get<RecyclingComponent>();
            }
        }

        private void GetBoundingData(ref SpriteRendererComponent spriteRendererComponent,
            ref TransformComponent transformComponent,
            out Vector2 objectHalfSize, out Vector2 absPosition, out Vector3 position)
        {
            Vector3 boundsSize = spriteRendererComponent.SpriteRenderer.bounds.size;
            objectHalfSize = new Vector2(boundsSize.x / 2, boundsSize.y / 2);
            position = transformComponent.Transform.position;
            absPosition = new Vector2(Mathf.Abs(position.x), Mathf.Abs(position.y));
        }
    }
}