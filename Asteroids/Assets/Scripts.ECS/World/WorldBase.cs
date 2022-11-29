using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.ECS.Components;
using Scripts.ECS.Entity;
using Scripts.ECS.Factories;
using Scripts.ECS.Pools;
using Scripts.PoolsAndFactories.Pools;

namespace Scripts.ECS.World
{
    public interface IGetControllers
    {
        T GetBehavior<T>();
    }

    public interface IInitBehavior
    {
        void Init(IGetControllers getControllers);
    }

    public abstract class WorldBase : IGetControllers
    {
        private Dictionary<Type, object> _behavioursByFunctionality =
            new Dictionary<Type, object>();

        private List<EntityBase> _entities =
            new List<EntityBase>(); //TODO Use dicitonary

        private Dictionary<Type, List<IComponent>> _components
            = new Dictionary<Type, List<IComponent>>();

        private List<IInitBehavior> _behaviors =
            new List<IInitBehavior>();

        protected IPool<EntityBase> _entityPool;

        public void Init()
        {
            _entityPool = new SimpleEntityPool<EntityBase>(new EntityFactory());

            for (int i = 0; i < _behaviors.Count; i++)
            {
                _behaviors[i].Init(this);
            }

            _behaviors = null;
        }

        public void AddBehavior(object behavior)
        {
            if(behavior is IInitBehavior initBehavior) 
                _behaviors.Add(initBehavior);

            var interfaces = behavior.GetType().GetInterfaces().
                Where(type => type != typeof(IInitBehavior)).ToArray();

            for (int i = 0; i < interfaces.Length; i++)
            {
                _behavioursByFunctionality[interfaces[i]] = behavior;
            }
        }
        
        public T GetBehavior<T>()
        {
            var type = typeof(T);
            if (_behavioursByFunctionality.TryGetValue(type, out var behavior))
                return (T)behavior;

            return default;
        }

        public T[] GetComponents<T>()
        {
            if (!_components.TryGetValue(typeof(T), out var components) | components is null)
                return Array.Empty<T>();

            return components.Cast<T>().ToArray();
        }

        public void AddEntity(EntityBase entity)
        {
            _entities.Add(entity);
        }

        public void RemoveEntity(EntityBase entity)
        {
            _entities.Remove(entity);
        }

        public EntityBase GetNewEntity()
        {
            return _entityPool.Get();
        }

        public EntityBase[] GetEntity<T>() where T : IComponent
        {
            return _entities.Where(entity => entity.GetComponent<T>() is { }).ToArray();
        }

        public EntityBase[] GetEntity<T1, T2>() where T1 : IComponent
            where T2 : IComponent
        {
            var afterT1Entities = GetEntity<T1>();

            if (afterT1Entities.Length <= 0)
                return afterT1Entities;

            return GetEntity<T2>(afterT1Entities);
        }

        public EntityBase[] GetEntity<T1, T2, T3>() where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
        {
            var afterT1Entities = GetEntity<T1>();

            if (afterT1Entities.Length <= 0)
                return afterT1Entities;

            var afterT2Entities = GetEntity<T2>(afterT1Entities);

            if (afterT2Entities.Length <= 0)
                return afterT2Entities;

            return GetEntity<T3>(afterT2Entities);
        }

        public virtual void Destroy()
        {
        }

        private EntityBase[] GetEntity<T>(EntityBase[] entities) where T : IComponent
        {
            return entities.Where(entity => entity.GetComponent<T>() is { }).ToArray();
        }
    }

    public static class Extensions
    {
        public static WorldBase InjectBehavior(this WorldBase world, object behavior)
        {
            world.AddBehavior(behavior);
            return world;
        }
        
        public static WorldBase InjectBehavior(this WorldBase world, object[] behaviors)
        {

            for (int i = 0; i < behaviors.Length; i++)
            {
                var behavior = behaviors[i];
                world.AddBehavior(behavior);
            }
            
            return world;
        }

        public static WorldBase Initialize(this WorldBase world)
        {
            world.Init();
            return world;
        }
    }

    public class World : WorldBase
    {
    }
}