using System;
using System.Threading.Tasks;
using Controllers;
using Scripts.CommonExtensions;
using Scripts.ECS.Components;
using Scripts.ECS.Entity;
using Scripts.ECS.Pools;
using Scripts.ECS.System;
using Scripts.ECS.World;
using Scripts.Main.Components;
using Scripts.Main.Controllers;
using Scripts.Main.Entities;
using Scripts.Main.Factories;
using Scripts.Main.Settings;
using Scripts.PoolsAndFactories.Pools;
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

    public class AsteroidsSpawnSystem : SystemBase
    {
        private bool delayActive;
        private IPool<EntityBase> _bigAsteroidsPool;
        private IPool<EntityBase> _smallAsteroidsPool;
        private Vector2 _screenBounds;

        public override void Init(WorldBase world)
        {
            base.Init(world);
            _screenBounds = _world.GetBehavior<IGetScreenBounds>().ScreenBounds;
            var loadAsteroids = _world.GetBehavior<ILoadAsteroids>();
            var bigAsteroidMonoEntityPrefab = loadAsteroids.LoadBigAsteroid().Load(runAsync: false).Result;
            var smallAsteroidMonoEntityPrefab = loadAsteroids.LoadSmallAsteroid().Load(runAsync: false).Result;
            _bigAsteroidsPool = new SimpleEntityPool<EntityBase>(
                new EntityToMonoFactory<BigAsteroidMonoEntity>(bigAsteroidMonoEntityPrefab, _world,
                    "BigAsteroidsHolder"));
            _smallAsteroidsPool = new SimpleEntityPool<EntityBase>(
                new EntityToMonoFactory<SmallAsteroidMonoEntity>(smallAsteroidMonoEntityPrefab, _world,
                    "SmallASteroidsHolder"));
        }

        public override void Run()
        {
            base.Run();
            Recycle();
            SpawnSmallASteroids();
            SpawnBigAsteroid();
        }

        private void Recycle()
        {
            var bigAsteroidsRecycleEntities = _world.GetEntity<RecyclingBigAsteroidComponent>();
            var smallAsteroidsRecycleEntities = _world.GetEntity<RecyclingSmallAsteroidComponent>();

            Recycle<RecyclingBigAsteroidComponent>(bigAsteroidsRecycleEntities, _bigAsteroidsPool);
            Recycle<RecyclingSmallAsteroidComponent>(smallAsteroidsRecycleEntities, _smallAsteroidsPool);
        }


        private void SpawnSmallASteroids()
        {
            var entities = _world.GetEntity<SpawnSmallAsteroidComponent>();

            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                entity.RemoveComponent<SpawnSmallAsteroidComponent>();
                entity.AddComponent(new RecyclingBigAsteroidComponent());
                var bigAsteroidTransformComponent = entity.GetComponent<TransformComponent>();
                var movableComponent = entity.GetComponent<MovableComponent>();

                if (movableComponent is null)
                    continue;

                var direction = movableComponent.Direction;

                for (int j = 0;
                     j < UnityEngine.Random.Range(1, RuntimeSharedData.GameSettings.MaxSmallAsteroidsSpawnCount);
                     j++)
                {
                    var myRotation = Quaternion.Euler(direction.x, direction.y,
                        direction.z + UnityEngine.Random.Range(
                            RuntimeSharedData.GameSettings.SmallAsteroidsSpawnCountDegrees.x,
                            RuntimeSharedData.GameSettings.SmallAsteroidsSpawnCountDegrees.y)); 
                    var resDirection = myRotation * direction;
                    var smallAsteroidEntity = _smallAsteroidsPool.Get();
                    var smallAsteroidMovableComponent = smallAsteroidEntity.GetComponent<MovableComponent>();
                    var transformComponent = smallAsteroidEntity.GetComponent<TransformComponent>();
                    if (transformComponent is { } && bigAsteroidTransformComponent is { })
                        transformComponent.Transform.position = bigAsteroidTransformComponent.Transform.position;

                    if (smallAsteroidMovableComponent is { })
                        smallAsteroidMovableComponent.Direction = new Vector3(resDirection.x, resDirection.y, 0f);
                    smallAsteroidEntity.GetComponent<GameObjectComponent>()?.GameObject.SetActiveOptimized(true);
                    _world.AddEntity(smallAsteroidEntity);
                }
            }
        }

        private void SpawnBigAsteroid()
        {
            if (delayActive)
                return;

            var bigAsteroidEntity = _bigAsteroidsPool.Get();
            var spawnSide = ExtensionsAndHelpers.GetOneRandom((Sides[])Enum.GetValues(typeof(Sides)));
            GetBoundsData(bigAsteroidEntity, out var objectHalfSize);
            var transformComponent = bigAsteroidEntity.GetComponent<TransformComponent>();
            var gameObjectComponent = bigAsteroidEntity.GetComponent<GameObjectComponent>();
            var movableComponent = bigAsteroidEntity.GetComponent<MovableComponent>();
            var spawnPosition = spawnSide switch
            {
                Sides.TopSide => new Vector3(
                    UnityEngine.Random.Range(-_screenBounds.x - objectHalfSize.x, _screenBounds.x + objectHalfSize.x),
                    _screenBounds.y + objectHalfSize.y, 0f),
                Sides.BottomSide => new Vector3(
                    UnityEngine.Random.Range(-_screenBounds.x - objectHalfSize.x, _screenBounds.x + objectHalfSize.x),
                    -_screenBounds.y - objectHalfSize.y, 0f),
                Sides.LeftSide => new Vector3(-_screenBounds.x - objectHalfSize.x,
                    UnityEngine.Random.Range(-_screenBounds.y - objectHalfSize.y, _screenBounds.y + objectHalfSize.y)
                    - _screenBounds.y - objectHalfSize.y, 0f),
                Sides.RightSide => new Vector3(_screenBounds.x + objectHalfSize.x,
                    UnityEngine.Random.Range(-_screenBounds.y - objectHalfSize.y, _screenBounds.y + objectHalfSize.y)
                    - _screenBounds.y - objectHalfSize.y, 0f),
                _ => new Vector3()
            };

            if (transformComponent is { })
                transformComponent.Transform.position = spawnPosition;

            if (gameObjectComponent is { })
                gameObjectComponent.GameObject.SetActiveOptimized(true);

            if (movableComponent is { })
            {
                movableComponent.Direction =
                    new Vector3(-spawnPosition.x, -spawnPosition.y, spawnPosition.z) - spawnPosition;
            }

            _world.AddEntity(bigAsteroidEntity);
            ProcessDelay();
        }

        private void GetBoundsData(EntityBase entity,
            out Vector2 objectHalfSize)
        {
            var spriteRendererComponent = entity.GetComponent<SpriteRendererComponent>();
            var boundsSize = spriteRendererComponent.SpriteRenderer.bounds.size;
            objectHalfSize = new Vector2(boundsSize.x / 2, boundsSize.y / 2);
        }

        private async void ProcessDelay()
        {
            delayActive = true;
            await Task.Delay(TimeSpan.FromSeconds(4f));
            delayActive = false;
        }
    }
}