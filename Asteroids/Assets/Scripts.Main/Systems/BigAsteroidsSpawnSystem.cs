using System;
using Leopotam.Ecs;
using Scripts.Main.Components;
using Scripts.Main.Loader;
using Scripts.Main.Pools;
using Scripts.Main.Settings;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class BigAsteroidsSpawnSystem : PausableSystem ,IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<BigAsteroidSystemComponent> _bigAsteroidSystemFilter;
        private EcsFilter<BigAsteroidSystemComponent, DelayComponent> _delayFilter;
        private EcsFilter<BigAsteroidSystemComponent, SetDelayComponent> _setDelayFilter;
        private EntityPoolProvider _poolProvider;
        private IEntityPool<EcsEntity, BigAsteroidComponent> _bigAsteroidsEntityPool;
        private ILoadAsteroids _loadAsteroids;
        private GameObject _bigAsteroidMonoEntityPrefab;
        private Transform _parent;

        public void Init()
        {
            _bigAsteroidsEntityPool = _poolProvider.Get<IEntityPool<EcsEntity, BigAsteroidComponent>>();
            var asteroidSystemEntity = _ecsWorld.NewEntity();
            asteroidSystemEntity.Get<BigAsteroidSystemComponent>();
            asteroidSystemEntity.Get<SetDelayComponent>();
            _parent = new GameObject("BigAsteroidsHolder").transform;
            _bigAsteroidMonoEntityPrefab = _loadAsteroids.LoadBigAsteroid().Load(runAsync: false).Result.gameObject;
        }

        protected override void Tick()
        {
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

                ref DelayComponent spawnDelayComponent = ref _delayFilter.Get2(0);
                if (DateTime.Now.TimeOfDay >= spawnDelayComponent.DelayTimer)
                {
                    delayEntity.Del<DelayComponent>();
                }

                return;
            }
            
            SetDelay();

            if (_bigAsteroidsEntityPool.EntityCount > 0)
            {
                ref var pooledEntity = ref _bigAsteroidsEntityPool.Get();
                pooledEntity.Get<EntityScreenPlacementComponent>();
                return;
            }
            
            
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