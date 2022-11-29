using Controllers;
using ECS.Systems;
using Scripts.CommonExtensions;
using Scripts.ECS.Components;
using Scripts.ECS.Entity;
using Scripts.ECS.Pools;
using Scripts.ECS.System;
using Scripts.ECS.World;
using Scripts.Main.Components;
using Scripts.Main.Entities;
using Scripts.Main.Factories;
using Scripts.PoolsAndFactories.Pools;

namespace Scripts.Main.Systems
{
    public class BulletSpawnSystem : SystemBase
    {
        private IPool<EntityBase> _pool;

        public override void Init(WorldBase world)
        {
            base.Init(world);
            var bulletPrefab = _world.GetBehavior<ILoadBullet>().LoadBullet().Load(runAsync: false).Result;
            _pool = new SimpleEntityPool<EntityBase>(new EntityToMonoFactory<BulletMonoEntity>(bulletPrefab, _world, "BulletHolder"));
        }

        public override void Run()
        {
            base.Run();
            
            Recycle();
            
            var shootBulletEntities = _world.GetEntity<ShootBulletComponent>();
            if (shootBulletEntities.Length == 0)
                return;

            for (int i = 0; i < shootBulletEntities.Length; i++)
            {
                var playerComponent = shootBulletEntities[i].GetComponent<PlayerComponent>();
                shootBulletEntities[i].GetComponent<ShootBulletComponent>(true);
                var bulletEntity = _pool.Get();
                var transformComponent = bulletEntity.GetComponent<TransformComponent>();
                if (transformComponent is { })
                {
                    transformComponent.Transform.position = playerComponent.ShootTransform.position;
                    transformComponent.Transform.rotation = playerComponent.ShootTransform.rotation;
                }

                var movableComponent = bulletEntity.GetComponent<MovableComponent>();
                bulletEntity.GetComponent<GameObjectComponent>().GameObject.SetActiveOptimized(true);
                movableComponent.Direction = playerComponent.ShootTransform.up;
                _world.AddEntity(bulletEntity);
            }
        }

        private void Recycle()
        {
            var entities = _world.GetEntity<RecyclingBulletComponent>();

            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                entity.RemoveComponent<RecyclingBulletComponent>();
                entity.GetComponent<GameObjectComponent>().GameObject.SetActiveOptimized(false);
                _world.RemoveEntity(entity);
                _pool.Return(entity);
            }
        }
    }
}