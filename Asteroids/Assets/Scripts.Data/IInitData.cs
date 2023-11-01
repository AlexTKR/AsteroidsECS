namespace Scripts.Data
{
    public interface IInitData
    {
        void Init(); 
    }

    public interface IDataRepository<T> : IInitData
    {
        T Data { get; }
    }
}
