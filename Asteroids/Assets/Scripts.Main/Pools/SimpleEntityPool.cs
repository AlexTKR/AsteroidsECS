using System;
using System.Collections.Generic;
using Scripts.CommonExtensions;

namespace Scripts.Main.Pools
{
    public class SimpleEntityPool<T, T1> : IEntityPool<T, T1> where T : struct
    where T1 : struct
    {
        private T[] _pooled;
        private int _entityCount;
        private float _resizeMult = 0.5f;

        public SimpleEntityPool()
        {
            _pooled = new T[20];
        }

        public int EntityCount => _entityCount;

        public ref T Get()
        {
            _entityCount--;
            ref var entity = ref _pooled[_entityCount];
            return ref entity;
        }

        public void Return(ref T entity)
        {
            if(_entityCount >= _pooled.Length)
                Array.Resize(ref _pooled, (int)(_pooled.Length + _pooled.Length * _resizeMult));
            
            _pooled[_entityCount] = entity;
            _entityCount++;
        }
    }
}