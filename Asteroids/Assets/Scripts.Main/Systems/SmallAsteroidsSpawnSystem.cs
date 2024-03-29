using Leopotam.Ecs;
using Scripts.Main.Components;
using Scripts.Main.Loader;
using Scripts.Main.Pools;
using Scripts.Main.Settings;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class SmallAsteroidsSpawnSystem : PausableSystem, IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<BigAsteroidComponent, DamageComponent, TransformComponent, MovableComponent> _asteroidsFilter;
        private EntityPoolProvider _poolProvider;
        private IEntityPool<EcsEntity, SmallAsteroidComponent> _smallAsteroidsEntityPool;
        private ILoadAsteroids _loadAsteroids;
        private GameObject _smallAsteroidMonoEntityPrefab;
        private Transform _parent;

        public void Init()
        {
            _smallAsteroidsEntityPool = _poolProvider.Get<IEntityPool<EcsEntity, SmallAsteroidComponent>>();
            _parent = new GameObject("SmallAsteroidsHolder").transform;
            _smallAsteroidMonoEntityPrefab = _loadAsteroids.LoadSmallAsteroid().Load(runAsync: false).Result.gameObject;
        }

        protected override void Tick()
        {
            foreach (var i in _asteroidsFilter)
            {
                ref var bigAsteroidEntity = ref _asteroidsFilter.GetEntity(i);
                ref TransformComponent bigAsteroidTransformComponent = ref _asteroidsFilter.Get3(i);
                ref MovableComponent movableComponent = ref _asteroidsFilter.Get4(i);
                var moveDirection = movableComponent.Direction;
                var bigAsteroidTransform = bigAsteroidTransformComponent.Transform;
                bigAsteroidEntity.Del<DamageComponent>();

                for (int j = 0;
                     j < Random.Range(1, RuntimeSharedData.GameSettings.MaxSmallAsteroidsSpawnCount);
                     j++)
                {
                    if (_smallAsteroidsEntityPool.EntityCount > 0)
                    {
                        ref var pooledEntity = ref _smallAsteroidsEntityPool.Get();
                        ref TransformComponent smallAsteroidTransformComponent = ref pooledEntity.Get<TransformComponent>();
                        ref MovableComponent smallAsteroidMovableComponent = ref pooledEntity.Get<MovableComponent>();
                        smallAsteroidMovableComponent.Direction = moveDirection;
                        var smallAsteroidTransform = smallAsteroidTransformComponent.Transform;
                        smallAsteroidTransform.position = bigAsteroidTransform.position;
                        pooledEntity.Get<EntityScreenPlacementComponent>();
                        continue;
                    }
                    
                    var spawnEntity = _ecsWorld.NewEntity();
                    spawnEntity.Get<MovableComponent>() = new MovableComponent()
                    {
                        Direction = moveDirection
                    };
                    spawnEntity.Get<SpawnComponent>() = new SpawnComponent()
                    {
                        Prefab = _smallAsteroidMonoEntityPrefab,
                        Position = bigAsteroidTransform.position,
                        Rotation = bigAsteroidTransform.rotation,
                        Parent = _parent,
                        IsActive = true
                    };

                    spawnEntity.Get<EntityScreenPlacementComponent>();
                }
            }
        }
    }
}