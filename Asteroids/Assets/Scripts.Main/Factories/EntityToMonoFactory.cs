using Scripts.ECS.Entity;
using Scripts.ECS.World;
using Scripts.Main.Converters;
using Scripts.PoolsAndFactories.Factories;
using UnityEngine;

namespace Scripts.Main.Factories
{
    public class EntityToMonoFactory<T> : IFactory<EntityBase>  where T : EntityToMono
    {
        private readonly T _entityPrefab;
        private WorldBase _world;
        private GameObject _objectsHolder; 
            
        public EntityToMonoFactory(T entityPrefab, WorldBase world, string holderName)
        {
            _objectsHolder = new GameObject(holderName);
            _entityPrefab = entityPrefab;
            _world = world;
        }
            
        public EntityBase Get()
        {
            var monoEntity = Object.Instantiate<T>(_entityPrefab, _objectsHolder.transform);
            return monoEntity.Convert(_world.GetNewEntity());
        }
    }
}
