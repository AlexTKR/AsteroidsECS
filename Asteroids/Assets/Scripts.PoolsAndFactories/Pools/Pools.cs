using System.Collections.Generic;

namespace Scripts.PoolsAndFactories.Pools
{
    public interface IPool<T>
    {
        T Get();
        void Return(T view);
    }
}