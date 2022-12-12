namespace Scripts.Main.Pools
{
    public interface IEntityPool<T, T1>
    {
        int EntityCount { get; }
        ref T Get();
        void Return(ref T view);
    }
}