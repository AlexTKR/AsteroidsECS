using Controllers;
using Leopotam.Ecs;
using Scripts.CommonExtensions;
using Scripts.Main.Components;
using Scripts.Main.Pools;
using UnityEngine;


namespace Scripts.Main.Systems
{
    public class BulletSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter<PlayerComponent> _playerFilter;
        private EcsFilter<ShootBulletComponent> _bulletFilter;
        private EcsWorld _ecsWorld;

        private ILoadBullet _loadBullet;
        private IEntityPool<EcsEntity, BulletComponent> _bulletEntityPool;
        private GameObject _bulletPrefab;
        private Transform _parent;

        public void Init()
        {
            _bulletPrefab = _loadBullet.LoadBullet().Load(runAsync: false).Result.gameObject;
            _parent = new GameObject("BulletHolder").transform;
        }

        public void Run()
        {
            if (_playerFilter.IsEmpty())
                return;

            ref var playerComponent = ref _playerFilter.Get1(0);
            var shootTransform = playerComponent.ShootTransform;

            foreach (var i in _bulletFilter)
            {
                ref var shootBulletEntity = ref _bulletFilter.GetEntity(i);
                shootBulletEntity.Del<ShootBulletComponent>();

                if (_bulletEntityPool.EntityCount > 0)
                {
                    ref var pooledEntity = ref _bulletEntityPool.Get();
                    if (!pooledEntity.Has<GameObjectComponent>() ||
                        !pooledEntity.Has<TransformComponent>() ||
                        !pooledEntity.Has<MovableComponent>())
                        continue;

                    ref var transformComponent = ref pooledEntity.Get<TransformComponent>();
                    ref var gameObjectComponent = ref pooledEntity.Get<GameObjectComponent>();
                    ref var movableComponent = ref pooledEntity.Get<MovableComponent>();

                    transformComponent.Transform.position = shootTransform.position;
                    transformComponent.Transform.rotation = shootTransform.rotation;
                    movableComponent.Direction = shootTransform.up;
                    gameObjectComponent.GameObject.SetActiveOptimized(true);
                    continue;
                }
                
                var spawnEntity = _ecsWorld.NewEntity();
                spawnEntity.Get<SpawnComponent>() = new SpawnComponent()
                {
                    Prefab = _bulletPrefab,
                    Position = shootTransform.position,
                    Rotation =  shootTransform.rotation,
                    Parent = _parent,
                    IsActive = true
                };
            }
        }
    }
}
