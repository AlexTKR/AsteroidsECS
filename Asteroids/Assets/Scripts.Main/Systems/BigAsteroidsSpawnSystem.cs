using System;
using Controllers;
using Leopotam.Ecs;
using Scripts.Main.Components;
using Scripts.Main.Pools;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class BigAsteroidsSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<AsteroidsSpawnDelayComponent> _spawnDelayFilter;
        private IEntityPool<EcsEntity, BigAsteroidComponent> _bigAsteroidsEntityPool;
        private ILoadAsteroids _loadAsteroids;
        private GameObject _bigAsteroidMonoEntityPrefab;
        private Transform _parent;

        public void Init()
        {
            _parent = new GameObject("BigAsteroidsHolder").transform;
            _bigAsteroidMonoEntityPrefab = _loadAsteroids.LoadBigAsteroid().Load(runAsync: false).Result.gameObject;
            SetDelay();
        }

        public void Run()
        {
            if (_spawnDelayFilter.IsEmpty())
            {
                if (_bigAsteroidsEntityPool.EntityCount > 0)
                {
                    ref var pooledEntity = ref _bigAsteroidsEntityPool.Get();
                    pooledEntity.Get<EntityScreenPlacementComponent>();
                }
                else
                {
                    var spawnEntity = _ecsWorld.NewEntity();
                    spawnEntity.Get<SpawnComponent>() = new SpawnComponent()
                    {
                        Prefab = _bigAsteroidMonoEntityPrefab,
                        Position = Vector3.zero,
                        Rotation = Quaternion.identity,
                        Parent = _parent
                    };
                    spawnEntity.Get<EntityScreenPlacementComponent>();
                }

                SetDelay();
                return;
            }

            ref var delayEntity = ref _spawnDelayFilter.GetEntity(0);
            ref var spawnDelayComponent = ref _spawnDelayFilter.Get1(0);

            if (DateTime.Now.TimeOfDay >= spawnDelayComponent.Delay)
            {
                delayEntity.Del<AsteroidsSpawnDelayComponent>();
            }
        }

        private void SetDelay()
        {
            _ecsWorld.NewEntity().Get<AsteroidsSpawnDelayComponent>() = new AsteroidsSpawnDelayComponent()
            {
                Delay = DateTime.Now.TimeOfDay.Add(TimeSpan.FromSeconds(2f)) //TODO MOVE to game sett
            };
        }
    }
}