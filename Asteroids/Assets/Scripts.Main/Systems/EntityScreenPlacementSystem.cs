using System;
using System.Threading.Tasks;
using Leopotam.Ecs;
using Scripts.CommonBehaviours;
using Scripts.CommonExtensions;
using Scripts.Main.Components;
using Scripts.Main.Controllers;
using Scripts.Main.Settings;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public enum Sides
    {
        TopSide,
        BottomSide,
        LeftSide,
        RightSide
    }

    public class EntityScreenPlacementSystem : IEcsRunSystem
    {
        private EcsFilter<EntityScreenPlacementComponent, TransformComponent,
            GameObjectComponent, MovableComponent, SpriteRendererComponent> _placementFilter;

        private IScreenBoundsProvider _screenBoundsProvider;


        public void Run()
        {
            if (IPauseBehaviour.IsPaused)
                return;
            
            ref var screenBounds = ref _screenBoundsProvider.ScreenBounds;

            foreach (var i in _placementFilter)
            {
                ref var placementEntity = ref _placementFilter.GetEntity(i);

                ref var transformComponent = ref _placementFilter.Get2(i);
                ref var gameObjectComponent = ref _placementFilter.Get3(i);
                ref var movableComponent = ref _placementFilter.Get4(i);
                ref var spriteRendererComponent = ref _placementFilter.Get5(i);

                placementEntity.Del<EntityScreenPlacementComponent>();

                if (placementEntity.Has<BigAsteroidComponent>() ||
                    placementEntity.Has<UfoComponent>())
                {
                    PlaceAtRandomScreenEdge(ref transformComponent, ref gameObjectComponent,
                        ref movableComponent, spriteRendererComponent, ref screenBounds);
                    continue;
                }

                if (placementEntity.Has<SmallAsteroidComponent>())
                {
                    PlaceInsideAndGetRandomDirection(ref transformComponent, ref gameObjectComponent,
                        ref movableComponent);
                    continue;
                }
                
                
            }
        }


        private void PlaceInsideAndGetRandomDirection(ref TransformComponent transformComponent,
            ref GameObjectComponent gameObjectComponent, ref MovableComponent movableComponent)
        {
            var direction = movableComponent.Direction;

            var myRotation = Quaternion.Euler(direction.x, direction.y,
                direction.z + UnityEngine.Random.Range(
                    RuntimeSharedData.GameSettings.SmallAsteroidsSpawnCountDegrees.x,
                    RuntimeSharedData.GameSettings.SmallAsteroidsSpawnCountDegrees.y));
            var resDirection = myRotation * direction;

            movableComponent.Direction = new Vector3(resDirection.x, resDirection.y, 0f);
            gameObjectComponent.GameObject.SetActiveOptimized(true);
        }

        private void PlaceAtRandomScreenEdge(ref TransformComponent transformComponent,
            ref GameObjectComponent gameObjectComponent, ref MovableComponent movableComponent,
            SpriteRendererComponent spriteRendererComponent, ref Vector2 screenBounds)
        {
            var spawnSide = ExtensionsAndHelpers.GetOneRandom((Sides[])Enum.GetValues(typeof(Sides)));
            GetBoundsData(ref spriteRendererComponent, out var objectHalfSize);
            var spawnPosition = spawnSide switch
            {
                Sides.TopSide => new Vector3(
                    UnityEngine.Random.Range(-screenBounds.x - objectHalfSize.x, screenBounds.x + objectHalfSize.x),
                    screenBounds.y + objectHalfSize.y, 0f),
                Sides.BottomSide => new Vector3(
                    UnityEngine.Random.Range(-screenBounds.x - objectHalfSize.x, screenBounds.x + objectHalfSize.x),
                    -screenBounds.y - objectHalfSize.y, 0f),
                Sides.LeftSide => new Vector3(-screenBounds.x - objectHalfSize.x,
                    UnityEngine.Random.Range(-screenBounds.y - objectHalfSize.y, screenBounds.y + objectHalfSize.y)
                    - screenBounds.y - objectHalfSize.y, 0f),
                Sides.RightSide => new Vector3(screenBounds.x + objectHalfSize.x,
                    UnityEngine.Random.Range(-screenBounds.y - objectHalfSize.y, screenBounds.y + objectHalfSize.y)
                    - screenBounds.y - objectHalfSize.y, 0f),

                _ => new Vector3()
            };

            transformComponent.Transform.position = spawnPosition;
            gameObjectComponent.GameObject.SetActiveOptimized(true);
            movableComponent.Direction =
                new Vector3(-spawnPosition.x, -spawnPosition.y, spawnPosition.z) - spawnPosition;
        }

        private void GetBoundsData(ref SpriteRendererComponent spriteRendererComponent,
            out Vector2 objectHalfSize)
        {
            var boundsSize = spriteRendererComponent.SpriteRenderer.bounds.size;
            objectHalfSize = new Vector2(boundsSize.x / 2, boundsSize.y / 2);
        }
    }
}