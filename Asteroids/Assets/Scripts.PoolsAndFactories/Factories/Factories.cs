namespace Scripts.PoolsAndFactories.Factories
{
    public interface IFactory<T>
    {
        T Get();
    }
}
