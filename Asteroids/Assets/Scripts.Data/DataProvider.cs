using System;
using System.Collections.Generic;

namespace Scripts.Data
{
    public interface IDataProvider
    {
        void Add<T>(IDataRepository<T> instance);
        IDataRepository<T> Get<T>();
    }

    public class DataProvider : IDataProvider
    {
        private Dictionary<Type, object> _container
            = new Dictionary<Type, object>();
        
        public void Add<T>(IDataRepository<T> instance)
        {
            instance.Init();
            _container[typeof(IDataRepository<T>)] = instance;
        }

        public IDataRepository<T> Get<T>()
        {
            if (!_container.TryGetValue(typeof(IDataRepository<T>), out var value))
                return default;

            return (IDataRepository<T>)value;
        }
    }
}