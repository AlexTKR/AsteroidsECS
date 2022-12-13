using System;
using Controllers;
using Leopotam.Ecs;
using Scripts.Main.Components;
using Scripts.Main.Pools;
using Scripts.Main.Settings;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class UfoSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<UfoSpawnDelayComponent> _spawnDelayFilter;
        private EcsFilter<PlayerComponent, TransformComponent> _playerFilter;
        private IEntityPool<EcsEntity, UfoComponent> _ufoEntityPool;
        private ILoadUfo _loadUfo;
        private GameObject _ufoMonoEntityPrefab;
        private Transform _parent;

        public void Init()
        {
            _parent = new GameObject("UfoHolder").transform;
            _ufoMonoEntityPrefab = _loadUfo.LoadUfo().Load(runAsync: false).Result.gameObject;
            SetDelay();
        }

        public void Run()
        {
            ref var playerTransformComponent = ref _playerFilter.Get2(0);

            if (!_spawnDelayFilter.IsEmpty())
            {
                ref var delayEntity = ref _spawnDelayFilter.GetEntity(0);

                ref var spawnDelayComponent = ref _spawnDelayFilter.Get1(0);
                if (DateTime.Now.TimeOfDay >= spawnDelayComponent.Delay)
                {
                    delayEntity.Del<UfoSpawnDelayComponent>();
                }
                
                return;
            }

            if (_ufoEntityPool.EntityCount > 0)
            {
                ref var pooledEntity = ref _ufoEntityPool.Get();
                pooledEntity.Get<EntityScreenPlacementComponent>();
            }
            else
            {
                var spawnEntity = _ecsWorld.NewEntity();
                spawnEntity.Get<FollowTargetComponent>() = new FollowTargetComponent()
                {
                    Target = playerTransformComponent.Transform
                };
                spawnEntity.Get<SpawnComponent>() = new SpawnComponent()
                {
                    Prefab = _ufoMonoEntityPrefab,
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
            _ecsWorld.NewEntity().Get<UfoSpawnDelayComponent>() = new UfoSpawnDelayComponent()
            {
                Delay = DateTime.Now.TimeOfDay.Add(TimeSpan.FromSeconds(RuntimeSharedData.GameSettings.UfoSpawnDelay)) 
            };
        }
    }
}