using Leopotam.Ecs;
using Scripts.CommonExtensions;
using Scripts.Main.Components;
using Scripts.Main.Pools;

namespace Scripts.Main.Systems
{
    public class RecyclingSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter<RecyclingComponent, GameObjectComponent> _recyclingFilter;
        private EntityPoolProvider _poolProvider;
        private IEntityPool<EcsEntity, BigAsteroidComponent> _bigAsteroidsEntityPool;
        private IEntityPool<EcsEntity, SmallAsteroidComponent> _smallAsteroidsEntityPool;
        private IEntityPool<EcsEntity, BulletComponent> _bulletEntityPool;
        private IEntityPool<EcsEntity, UfoComponent> _ufoEntityPool;

        public void Init()
        {
            _bigAsteroidsEntityPool = _poolProvider.Get<IEntityPool<EcsEntity, BigAsteroidComponent>>();
            _smallAsteroidsEntityPool = _poolProvider.Get<IEntityPool<EcsEntity, SmallAsteroidComponent>>();
            _bulletEntityPool = _poolProvider.Get<IEntityPool<EcsEntity, BulletComponent>>();
            _ufoEntityPool = _poolProvider.Get<IEntityPool<EcsEntity, UfoComponent>>();
        }
        
        public void Run()
        {
            foreach (var i in _recyclingFilter)
            {
                ref var recyclingEntity = ref _recyclingFilter.GetEntity(i);
                ref var gameObjectComponent = ref _recyclingFilter.Get2(i);
                gameObjectComponent.GameObject.SetActiveOptimized(false);

                if (recyclingEntity.Has<BulletComponent>())
                {
                    _bulletEntityPool.Return(ref recyclingEntity);
                    recyclingEntity.Del<RecyclingComponent>();
                    continue;
                }

                if (recyclingEntity.Has<BigAsteroidComponent>())
                {
                    _bigAsteroidsEntityPool.Return(ref recyclingEntity);
                    recyclingEntity.Del<RecyclingComponent>();
                    continue;
                }

                if (recyclingEntity.Has<SmallAsteroidComponent>())
                {
                    _smallAsteroidsEntityPool.Return(ref recyclingEntity);
                    recyclingEntity.Del<RecyclingComponent>();
                    continue;
                }
                
                if (recyclingEntity.Has<UfoComponent>())
                {
                    _ufoEntityPool.Return(ref recyclingEntity);
                    recyclingEntity.Del<RecyclingComponent>();
                }
            }
        }
    }
}