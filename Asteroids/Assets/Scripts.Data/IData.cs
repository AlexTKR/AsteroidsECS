namespace Scripts.Data
{
    public interface IDataRepository<T>
    {
        T Data { get; }
        
        void Init(); 
    }
}
