using System;
using System.Collections.Generic;

namespace Scripts.CommonBehaviours
{
    public interface IProvider
    {
        void Add<T>(T instance);
        T Get<T>();
    }

    public class Provider
    {
        protected Dictionary<Type, object> _container
            = new Dictionary<Type, object>();
        
        public virtual void Add<T>(T instance)
        {
            _container[typeof(T)] = instance;
        }

        public virtual T Get<T>()
        {
            if (!_container.TryGetValue(typeof(T), out var value))
                return default;

            return (T)value;
        }
    }
}