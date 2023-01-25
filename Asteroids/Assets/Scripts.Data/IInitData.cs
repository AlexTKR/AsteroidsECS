namespace Scripts.Data
{
    public interface IInitData
    {
        void Init(); //Provide data source here, for instance db source or server
    }

    public interface IDataRepository<T> : IInitData
    {
        T Data { get; }
    }
}
