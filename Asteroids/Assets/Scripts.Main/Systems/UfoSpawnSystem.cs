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
    public class UfoSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<UfoSystemComponent> _ufoSystemFilter;
        private EcsFilter<UfoSystemComponent, DelayComponent> _delayFilter;
        private EcsFilter<UfoSystemComponent, SetDelayComponent> _setDelayFilter;
        private EcsFilter<PlayerComponent, TransformComponent> _playerFilter;
        private IEntityPool<EcsEntity, UfoComponent> _ufoEntityPool;
        private ILoadUfo _loadUfo;
        private GameObject _ufoMonoEntityPrefab;
        private Transform _parent;

        public void Init()
        {
            var ufoSystemEntity = _ecsWorld.NewEntity();
            ufoSystemEntity.Get<UfoSystemComponent>();
            ufoSystemEntity.Get<SetDelayComponent>();
            _parent = new GameObject("UfoHolder").transform;
            _ufoMonoEntityPrefab = _loadUfo.LoadUfo().Load(runAsync: false).Result.gameObject;
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
            
            if (!_setDelayFilter.IsEmpty())
            {
                ref var setDelayEntity = ref _setDelayFilter.GetEntity(0);
                setDelayEntity.Del<SetDelayComponent>();
                SetDelay();
                return;
            }

            ref var playerTransformComponent = ref _playerFilter.Get2(0);

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
            ref var delayEntity = ref _ufoSystemFilter.GetEntity(0);
            delayEntity.Get<DelayComponent>() = new DelayComponent()
            {
                DelayTimer =
                    DateTime.Now.TimeOfDay.Add(TimeSpan.FromSeconds(RuntimeSharedData.GameSettings.UfoSpawnDelay))
            };
        }
    }
}