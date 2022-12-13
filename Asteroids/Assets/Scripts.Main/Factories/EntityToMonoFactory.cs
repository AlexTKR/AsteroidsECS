using System.Linq;
using Leopotam.Ecs;
using Scripts.CommonExtensions;
using Scripts.Main.Components;
using Scripts.Main.Converters;
using UnityEngine;

namespace Scripts.Main.Factories
{
    public class EntityToMonoFactory : IFactory<EcsEntity, SpawnComponent>
    {
        private EcsWorld _world;

        public EntityToMonoFactory(EcsWorld world)
        {
            _world = world;
        }

        public void Get(ref EcsEntity entity, ref SpawnComponent inData)
        {
            var spawnObject = Object.Instantiate(inData.Prefab, inData.Position, inData.Rotation, inData.Parent);

            var entityToMono = spawnObject.GetComponent<EntityToMono>();
            var entityToMonoChildren = spawnObject.GetComponentsInChildren<EntityToMono>(true)
                .Where(mono => mono != entityToMono).ToArray();

            for (int i = 0; i < entityToMonoChildren.Length; i++)
            {
                var childEntity = _world.NewEntity();
                entityToMonoChildren[i].Convert(ref childEntity);
            }

            if (entityToMono is { })
                entityToMono.Convert(ref entity);

            spawnObject.SetActiveOptimized(inData.IsActive);
        }
    }
}