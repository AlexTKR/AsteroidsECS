using System;
using System.Collections.Generic;
using Scripts.CommonExtensions;

namespace Scripts.Main.Pools
{
    public class SimpleEntityPool<T, T1> : IEntityPool<T, T1> where T : struct
    where T1 : struct
    {
        private T[] _pooled;
        private int _entityEntityCount;

        public SimpleEntityPool()
        {
            _pooled = new T[20];
        }

        public int EntityCount => _entityEntityCount;

        public ref T Get()
        {
            _entityEntityCount--;
            ref var entity = ref _pooled[_entityEntityCount];
            return ref entity;
        }

        public void Return(ref T entity)
        {
            if(_entityEntityCount >= _pooled.Length)
                Array.Resize(ref _pooled, (int)(_pooled.Length + _pooled.Length * 0.5));
            
            _pooled[_entityEntityCount] = entity;
            _entityEntityCount++;
        }
    }
}