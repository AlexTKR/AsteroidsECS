using System.Collections.Generic;
using Scripts.CommonExtensions;
using Scripts.ECS.Entity;
using Scripts.PoolsAndFactories.Factories;
using Scripts.PoolsAndFactories.Pools;

namespace Scripts.ECS.Pools
{
    public class SimpleEntityPool<T> : IPool<T> where T : EntityBase
    {
        private List<T> _pooled;
        private IFactory<EntityBase> _factory;


        public SimpleEntityPool(IFactory<EntityBase> factory)
        {
            _pooled = new List<T>();
            _factory = factory;
        }

        public T Get()
        {
            T element;
            if (_pooled.Count > 0)
                element = _pooled.RemoveAtAndReturn(0);
            else
                element = (T)_factory.Get();

            return element;
        }

        public void Return(T view)
        {
            _pooled.Add(view);
        }
    }
}