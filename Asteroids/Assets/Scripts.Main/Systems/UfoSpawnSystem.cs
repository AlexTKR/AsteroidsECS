using System;
using System.Threading.Tasks;
using Controllers;
using ECS.Systems;
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
using Scripts.PoolsAndFactories.Pools;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class UfoSpawnSystem : SystemBase
    {
        private bool delayActive;
        private IPool<EntityBase> _ufoPool;
        private Vector2 _screenBounds;

        public override void Init(WorldBase world)
        {
            base.Init(world);
            _screenBounds = _world.GetBehavior<IGetScreenBounds>().ScreenBounds;
            var loadUfo = _world.GetBehavior<ILoadUfo>();
            var ufoMonoEntityPrefab = loadUfo.LoadUfo().Load(runAsync: false).Result;
            _ufoPool = new SimpleEntityPool<EntityBase>(
                new EntityToMonoFactory<UfoMonoEntity>(ufoMonoEntityPrefab, _world, "UfoHolder"));
        }

        public override void Run()
        {
            base.Run();
            Recycle<RecyclingUfoComponent>(_world.GetEntity<RecyclingUfoComponent>(), _ufoPool);
            SpawnUfo();
        }

        private void SpawnUfo()
        {
            if (delayActive)
                return;

            var ufoEntity = _ufoPool.Get();
            var spawnSide = ExtensionsAndHelpers.GetOneRandom((Sides[])Enum.GetValues(typeof(Sides)));
            GetBoundsData(ufoEntity, out var objectHalfSize);
            var transformComponent = ufoEntity.GetComponent<TransformComponent>();
            var gameObjectComponent = ufoEntity.GetComponent<GameObjectComponent>();
            var followTargetComponent = ufoEntity.GetComponent<FollowTargetComponent>();
            var playerTransform = _world.GetEntity<PlayerComponent>().FirstIfAny()?.GetComponent<TransformComponent>();
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

            if (followTargetComponent is { } && playerTransform is { })
            {
                followTargetComponent.Target = playerTransform.Transform;
            }

            _world.AddEntity(ufoEntity);
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