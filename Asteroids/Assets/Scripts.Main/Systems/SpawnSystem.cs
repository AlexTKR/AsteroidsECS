using Leopotam.Ecs;
using Scripts.Main.Components;
using Scripts.Main.Factories;

namespace Scripts.Main.Systems
{
    public class SpawnSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter<SpawnComponent> _spawnFilter;

        private IFactory<EcsEntity, SpawnComponent> _factory;

        public void Init()
        {
            _factory = new EntityToMonoFactory(_world);
        }

        public void Run()
        {
            if (_spawnFilter.IsEmpty())
                return;

            foreach (var i in _spawnFilter)
            {
                ref var entity = ref _spawnFilter.GetEntity(i);
                ref SpawnComponent spawnComponent = ref _spawnFilter.Get1(i);
                _factory.Get(ref entity, ref spawnComponent);
                spawnComponent.OnSpawn?.Invoke();
                entity.Del<SpawnComponent>();
            }
        }
    }
}