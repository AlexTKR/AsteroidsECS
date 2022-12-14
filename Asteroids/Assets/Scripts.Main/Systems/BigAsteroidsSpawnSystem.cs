using System;
using Controllers;
using Leopotam.Ecs;
using Scripts.CommonBehaviours;
using Scripts.Main.Components;
using Scripts.Main.Pools;
using Scripts.Main.Settings;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class BigAsteroidsSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<BigAsteroidSystemComponent> _bigAsteroidSystemFilter;
        private EcsFilter<BigAsteroidSystemComponent, DelayComponent> _delayFilter;
        private EcsFilter<BigAsteroidSystemComponent, SetDelayComponent> _setDelayFilter;
        private IEntityPool<EcsEntity, BigAsteroidComponent> _bigAsteroidsEntityPool;
        private ILoadAsteroids _loadAsteroids;
        private GameObject _bigAsteroidMonoEntityPrefab;
        private Transform _parent;

        public void Init()
        {
            var asteroidSystemEntity = _ecsWorld.NewEntity();
            asteroidSystemEntity.Get<BigAsteroidSystemComponent>();
            asteroidSystemEntity.Get<SetDelayComponent>();
            _parent = new GameObject("BigAsteroidsHolder").transform;
            _bigAsteroidMonoEntityPrefab = _loadAsteroids.LoadBigAsteroid().Load(runAsync: false).Result.gameObject;
        }

        public void Run()
        {
            if (IPauseBehaviour.IsPaused)
                return;

            if (!_setDelayFilter.IsEmpty())
            {
                ref var setDelayEntity = ref _setDelayFilter.GetEntity(0);
                setDelayEntity.Del<SetDelayComponent>();
                SetDelay();
                return;
            }

            if (!_delayFilter.IsEmpty())
            {
                ref var delayEntity = ref _delayFilter.GetEntity(0);

                ref var spawnDelayComponent = ref _delayFilter.Get2(0);
                if (DateTime.Now.TimeOfDay >= spawnDelayComponent.DelayTimer)
                {
                    delayEntity.Del<DelayComponent>();
                }

                return;
            }

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
        }

        private void SetDelay()
        {
            ref var delayEntity = ref _bigAsteroidSystemFilter.GetEntity(0);
            delayEntity.Get<DelayComponent>() = new DelayComponent
            {
                DelayTimer = DateTime.Now.TimeOfDay.Add(
                    TimeSpan.FromSeconds(RuntimeSharedData.GameSettings.AsteroidsSpawnDelay))
            };
        }
    }
}